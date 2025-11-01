using AutoMapper;
using DataBaseManager.DbContexts;
using DataBaseManager.Extensions;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Dtos.Inventory;
using Shared.Dtos.Sales;
using Shared.Enum;
using Shared.Exceptions;
using Shared.Models.DataBaseModels.Account;
using Shared.Models.DataBaseModels.Inventory;
using Shared.Models.DataBaseModels.Sales;
using System.Linq;

namespace DataBaseManager.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public partial class SalesController : AppControllerBase
    {

        [HttpGet, EnableQuery]
        public IQueryable<SalesDto> Get()
        {
            return _appDbContext.Sales.Project<SalesDto>();
        }


        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<SalesDto>>>> GetSales(ODataQueryOptions<SalesDto> odataQuery , CancellationToken cancellationToken)
        {
            try
            {
                var query = (IQueryable<SalesDto>)odataQuery.ApplyTo(Get(), ignoreQueryOptions: AllowedQueryOptions.Top | AllowedQueryOptions.Skip);

                var totalCount = await query.LongCountAsync(cancellationToken);

                if (odataQuery.Skip is not null)
                    query = query.Skip(odataQuery.Skip.Value);

                if (odataQuery.Top is not null)
                    query = query.Take(odataQuery.Top.Value);

                return ApiResponse<PagedResult<SalesDto>>.Success("Sales fetched successfully", System.Net.HttpStatusCode.OK,new PagedResult<SalesDto>(await query.ToArrayAsync(cancellationToken), totalCount));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error adding sales");
                return ApiResponse<PagedResult<SalesDto>>.Success($"Failed to fetched sales \n chech logs...", System.Net.HttpStatusCode.OK);

            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<SalesDto>>> Get(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var dto = await Get().FirstOrDefaultAsync(t => t.Id == id, cancellationToken) ?? throw new ResourceNotFoundException($"Sale reord with id : {id} NotFound!");

                return ApiResponse<SalesDto>.Success("Sale fetched successfully", System.Net.HttpStatusCode.OK, dto);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error adding sales , error type : {ex.GetType()}");
                return ApiResponse<SalesDto>.Success($"Failed to fetched sales \n check logs...", System.Net.HttpStatusCode.OK);
            }
          
        }

            [HttpPost]
            public async Task<ActionResult<ApiResponse<SalesDto>>> Create(SalesDto dto, CancellationToken cancellationToken)
            {
                var transition = await _appDbContext.Database.BeginTransactionAsync();
                try
                {
                    var productTypeCount = new ProductDto[] { dto.Carpet!, dto.Rug!, dto.RawMaterial! }.Count(x => x is not null);

                    if (!(dto.ProductId == Guid.Empty || dto.ProductId == null) && productTypeCount > 0)
                        throw new InvalidOperationException("Cannot specify both ProductId and a product detail (Carpet/Rug/RawMaterial).");

                    if ((dto.ProductId == Guid.Empty || dto.ProductId == null) && productTypeCount != 1)
                        throw new InvalidOperationException("When ProductId is not provided, exactly one product detail (Carpet, Rug, or RawMaterial) must be present.");

                    dto.Id = new Guid();
                    var entityToAdd = _mapper.Map<Sales>(dto);

                    await _appDbContext.Sales.AddAsync(entityToAdd, cancellationToken);
                    await _appDbContext.SaveChangesAsync(cancellationToken);

                    await transition.CommitAsync();
                    return ApiResponse<SalesDto>.Success("Sales added successfully", System.Net.HttpStatusCode.OK, _mapper.Map<SalesDto>(entityToAdd));
                }
                catch (Exception ex)
                    {
                    await transition.RollbackAsync();
                    _logger.LogError(ex, "Error adding sales");
                    return ApiResponse<SalesDto>.Fail("Failed to add sales", System.Net.HttpStatusCode.InternalServerError);
                }
                finally
                {
                    await transition.DisposeAsync();
                }

            }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<SalesDto>>> Update(SalesDto dto, CancellationToken cancellationToken)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var entityToUpdate = await _appDbContext.Sales.FirstOrDefaultAsync(s => s.Id == dto.Id, cancellationToken) ?? throw new ResourceNotFoundException("") ;

                _mapper.Map(dto, entityToUpdate);

                await _appDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                var updatedDto = _mapper.Map<SalesDto>(entityToUpdate);
                return ApiResponse<SalesDto>.Success("Sales updated successfully", System.Net.HttpStatusCode.OK, updatedDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error updating sales");
                return ApiResponse<SalesDto>.Fail("Failed to update sales", System.Net.HttpStatusCode.InternalServerError);
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }


        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(Guid id, CancellationToken cancellationToken)
        {
            await using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _appDbContext.Sales.Remove(new() { Id = id });
                var affectedRows = (await _appDbContext.SaveChangesAsync(cancellationToken));

                if (affectedRows < 1)
                    throw new ResourceNotFoundException($"Sale reord with id : {id} NotFound!");
                return ApiResponse.Success("sale record deleted successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "Error deleting sales");
                return ApiResponse.Fail("Failed to delete sales", System.Net.HttpStatusCode.InternalServerError);
            }
            finally
            {
                await transaction.DisposeAsync();
            }

        }
        /*
        [HttpPost]
        public async Task<ActionResult<ApiResponse<SalesPageResponsDto>>> GetAllSales([FromBody] SalesPageRequestDto request) 
        { 
            var transition = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                var duration = request.DateFilter.EndTime - request.DateFilter.StartTime;
                
                
                var item_sold = await _appDbContext.Sales.Where(i => i.Date >= request.DateFilter.StartTime && i.Date <= request.DateFilter.EndTime).Include(p=> p.Price).Include(i=>i.Invoice).Include(p=>p.Product).ToListAsync();
                var item_sold_befor = await _appDbContext.Sales.Where(i => i.Date >= (request.DateFilter.StartTime - duration) && i.Date <= request.DateFilter.StartTime).Include(p => p.Price).Include(i => i.Invoice).Include(p => p.Product).ToListAsync();
                
                
                var returnd_item = await _appDbContext.Accounts.Where(i=> i.Name == "J برگشت از فروش بازرگانی فرش" && (i.Date>=request.DateFilter.StartTime && i.Date<=request.DateFilter.EndTime) ).ToListAsync();
                var returnd_item_before = await _appDbContext.Accounts.Where(i => i.Name == "J برگشت از فروش بازرگانی فرش" && (i.Date >= (request.DateFilter.StartTime - duration) && i.Date <= request.DateFilter.StartTime)).ToListAsync();
                
                var soldItem = item_sold.Select(p => new SoldItem
                {
                    Type = p.Product.GetType().ToString(),
                    Value = p.Price.Receipt
                }).ToList();
                var returnItem = (await Task.WhenAll(returnd_item.Select(async p =>
                {
                    var num = int.Parse(p.ArticleDescription!.Split(" ")[1]);
                    return new ReturnItem
                    {
                        Type = (await _appDbContext.Products.Where(p =>
                        p.ProductCode == (_appDbContext.Receipts.Where(R => R.number == num).FirstOrDefault()!.ProductCode)).FirstOrDefaultAsync())?.GetType().ToString()!,
                        Value = p.Debit
                    };
                }))).ToList();

                //CustomerCategorizedSalesDto test = await _appDbContext.Sales.Where(i => i.Date >= request.DateFilter.StartTime && i.Date <= (request.DateFilter.StartTime + request.DateFilter.TimeFrameUnit switch
                //{
                //    TimeFrameUnit.Day => TimeSpan.FromDays(1),
                //    TimeFrameUnit.Week => TimeSpan.FromDays(7),
                //    TimeFrameUnit.Month => TimeSpan.FromDays(30),
                //    TimeFrameUnit.Year => TimeSpan.FromDays(365),
                //    _ => TimeSpan.Zero
                //})).GroupBy(c =>c.Invoice.); // group them by there category ...


                var respons = new SalesPageResponsDto
                {
                    SalesSummary = new FinancialSummaryDto
                    {
                        SoldItems = soldItem,
                        ReturnItems = returnItem,

                        Sum = soldItem.Sum(x=>x.Value) + returnItem.Sum(x=>x.Value),
                        Count = soldItem.Count + returnItem.Count,

                        TotalSales = new Card
                        {
                            Value = soldItem.Sum(x=>x.Value),
                            Growth = ((soldItem.Sum(x=>x.Value) - item_sold_befor.Sum(x=>x.Price.Receipt)) / item_sold_befor.Sum(x => x.Price.Receipt))
                        },
                        ReturnTotal = new Card
                        {
                            Value = returnItem.Sum(x=>x.Value),
                            Growth = ((returnItem.Sum(x=>x.Value) - returnd_item_before.Sum(x => x.Debit)) / returnd_item_before.Sum(x => x.Debit))
                        },
                        NewModelsSales= new Card
                        {

                        },
                        OffSales = new Card
                        {

                        }
                    },
                    CategorizedSales = new CategorizedSales // fill this using the quereis ....
                    {
                        Sales = null
                    }
                };




                await transition.CommitAsync();
                return ApiResponse<SalesPageResponsDto>.Success("Sales added successfully", System.Net.HttpStatusCode.OK , respons);
            }
            catch (Exception ex)
            {
                await transition.RollbackAsync();
                _logger.LogError(ex, "Error adding sales");
                return ApiResponse<SalesPageResponsDto>.Fail("Failed to add sales", System.Net.HttpStatusCode.InternalServerError);
            }
            finally
            {
                await transition.DisposeAsync();
            }
        }
        */
    }
}
