using AutoMapper;
using DataBaseManager.DbContexts;
using DataBaseManager.Extensions;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.EntityFrameworkCore;
using Shared.DataBaseManagerControllerInterfaces.Sales;
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
    public partial class SalesController : AppControllerBase , ISalesController
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
    }
}
