using BineshSoloution.Dtos;
using BineshSoloution.Dtos.Panel;
using BineshSoloution.Dtos.Panel.Sales;
using BineshSoloution.Enum;
using BineshSoloution.Interfaces.Account;
using BineshSoloution.Interfaces.Products;
using BineshSoloution.Interfaces.Sales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.UriParser;

using System.Threading;

namespace BineshSoloution.Controllers.Panel;


// you have to implement a service layer later ....
[ApiController , Route("api/[controller]/[action]")]
public partial class SalesApiController : AppControllerBase
{
        [AutoInject] protected readonly ILogger<SalesApiController> _logger = default!;
    
    
    //Needs Optimizing ... 
    [HttpPost]
    public async Task<ActionResult<ApiResponse<SalesSummaryDto>>> GetSalesSummaryAsync([FromBody] SalesPageRequestDto request , CancellationToken cancellationToken)
    {
        try
        {
            var duration = request.DateFilter.EndTime - request.DateFilter.StartTime;
            
            var returedAccounts = await _AccountService.GetByNameAsync(_appSettings.ShalliSettings.AccountNames.RestoredItemsAccount, cancellationToken);

            var Sales = await _SalesService.GetByDateDiffrenceAsync(request.DateFilter.StartTime, request.DateFilter.EndTime, cancellationToken);
            var SalesBefor = await _SalesService.GetByDateDiffrenceAsync(request.DateFilter.StartTime - duration, request.DateFilter.StartTime, cancellationToken);

            var returnd = returedAccounts!.SubAccounts.Where(i => i.Date >= (request.DateFilter.StartTime - duration)).ToList();
            var returndbefore = returedAccounts!.SubAccounts.Where(i => i.Date >= (request.DateFilter.StartTime - duration) && i.Date <= request.DateFilter.StartTime).ToList();

            var soldItem = (await Task.WhenAll(Sales.Select(async p => new SoldItem
            {
                Type =  (await _ProductService.GetByIdAsync(p.ProductId , cancellationToken))!.GetType().ToString(),
                Value = p.Price.Receipt
            }))).ToList();

            //needs more thinking.. are we going to solve this in the logic , ir in the mapping ( its better to be solved in the mapping phase ... )
            var returnItem = (await Task.WhenAll(returnd.Select(async p =>
            {
                var num = int.Parse(p.ArticleDescription!.Split(" ")[1]);
                return new ReturnItem
                {
                    Type = (await _appDbContext.Products.Where(p =>
                    p.ProductCode == (_appDbContext.Receipts.Where(R => R.Number == num).FirstOrDefault()!.ProductCode)).FirstOrDefaultAsync())?.GetType().ToString()!,
                    Value = p.Debit
                };
            }))).ToList();

            var response = new SalesSummaryDto
            {
                SoldItems = soldItem,
                ReturnItems = returnItem,
                Count = 0, // is it the sales itself or all of the returndes and sales ? ,
                Sum = 0,
            };

            return ApiResponse<SalesSummaryDto>.Success("Sales Fetched successfully", System.Net.HttpStatusCode.OK, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding sales");
            return ApiResponse<SalesSummaryDto>.Fail("Failed to add sales", System.Net.HttpStatusCode.InternalServerError);
        }
        finally
        {
        }
    }
    //Needs Optimizing ... 
    [HttpPost] 
    public async Task<ActionResult<ApiResponse<SalesCardsDto>>> GetSalesCardAsync([FromBody] SalesPageRequestDto request , CancellationToken cancellationToken)
    {
        try
        {
            var duration = request.DateFilter.EndTime - request.DateFilter.StartTime;

            var returedAccounts = await _AccountService.GetByNameAsync(_appSettings.ShalliSettings.AccountNames.RestoredItemsAccount, cancellationToken);

            var Sales = await _SalesService.GetByDateDiffrenceAsync(request.DateFilter.StartTime, request.DateFilter.EndTime, cancellationToken);
            var SalesBefor = await _SalesService.GetByDateDiffrenceAsync(request.DateFilter.StartTime - duration, request.DateFilter.StartTime, cancellationToken);

            var returnd = returedAccounts!.SubAccounts.Where(i => i.Date >= (request.DateFilter.StartTime - duration)).ToList();
            var returndbefore = returedAccounts!.SubAccounts.Where(i => i.Date >= (request.DateFilter.StartTime - duration) && i.Date <= request.DateFilter.StartTime).ToList();

            var soldItem = (await Task.WhenAll(Sales.Select(async p => new SoldItem
            {
                Type = (await _ProductService.GetByIdAsync(p.ProductId, cancellationToken))!.GetType().ToString(),
                Value = p.Price.Receipt
            }))).ToList();
            //needs more thinking.. are we going to solve this in the logic , ir in the mapping ( its better to be solved in the mapping phase ... )
            var returnItem = (await Task.WhenAll(returnd.Select(async p =>
            {
                var num = int.Parse(p.ArticleDescription!.Split(" ")[1]);
                return new ReturnItem
                {
                    Type = (await _appDbContext.Products.Where(p =>
                    p.ProductCode == (_appDbContext.Receipts.Where(R => R.Number == num).FirstOrDefault()!.ProductCode)).FirstOrDefaultAsync())?.GetType().ToString()!,
                    Value = p.Debit
                };
            }))).ToList();

            var response = new SalesCardsDto
            {
                TotalSales = new Card
                {
                    Growth = CalculateGrowth(soldItem.Sum(x => x.Value) , SalesBefor.Sum(x => x.Price.Receipt)),
                    Value = soldItem.Sum(x=>x.Value)
                },
                ReturnTotal = new Card
                {
                    Growth = CalculateGrowth(returnItem.Sum(x=>x.Value) , returndbefore.Sum(x=>x.Debit)),

                },
                NewModelsSales = new Card
                {

                },
                OffSales = new Card
                {

                }
            };

            return ApiResponse<SalesCardsDto>.Success("Sales Fetched successfully", System.Net.HttpStatusCode.OK, response);
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Error adding sales");
            return ApiResponse<SalesCardsDto>.Success("Failed to add sales", System.Net.HttpStatusCode.InternalServerError);

        }
    }
    //Optimized ! 
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CategorizedSales>>> GetCustomercategorizedSalesAsync([FromBody] SalesPageRequestDto request , CancellationToken cancellationToken)
    {
        try
        {
            var duration = request.DateFilter.EndTime - request.DateFilter.StartTime;

            var Sales = await _SalesService.GetByDateDiffrenceAsync(request.DateFilter.StartTime, request.DateFilter.EndTime, cancellationToken);

            var response = new CategorizedSales
            {
                Sales = (await Task.WhenAll(Sales.GroupBy(i => new
                {
                    i.Invoice.Counterparty!.Type,
                    TimeFrame = GetTimeFrameStart(i.Date, request.DateFilter.TimeFrameUnit)

                }).Select(async group => new CategorizedCustmer
                {

                    Type = group.Key.Type,
                    Count = group.Count(),
                    OnDate = group.Key.TimeFrame
                }))).ToList()
            };

            return ApiResponse<CategorizedSales>.Success("Sales Fetched successfully", System.Net.HttpStatusCode.OK, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching sales");
            return ApiResponse<CategorizedSales>.Fail("Failed to fetch sales", System.Net.HttpStatusCode.InternalServerError);
        }
        finally
        {
        }
    }
    //Optimized !
    [HttpPost] 
    public async Task<ActionResult<ApiResponse<RegionalSalesDto>>> GetProvinceategorizdeSalesAsync([FromBody] SalesPageRequestDto request , CancellationToken cancellationToken)
    {
        try
        {
            var duration = request.DateFilter.EndTime - request.DateFilter.StartTime;
            var Sales = await _SalesService.GetByDateDiffrenceAsync(request.DateFilter.StartTime, request.DateFilter.EndTime, cancellationToken);

            var currentSales = Sales
                .Where(i => i.Date >= request.DateFilter.StartTime && i.Date <= request.DateFilter.EndTime)
                .Where(i => i.Invoice.Counterparty!.Person.Region.Province == request.Provience.Provinece)
                .ToList();

            var previousSales = Sales
                .Where(i => i.Date >= request.DateFilter.StartTime - duration && i.Date < request.DateFilter.StartTime)
                .Where(i => i.Invoice.Counterparty!.Person.Region.Province == request.Provience.Provinece)
                .ToList();

            var previousByCity = previousSales
                .GroupBy(i => i.Invoice.Counterparty!.Person.Region.City!)
                .ToDictionary(g => g.Key, g => g.Sum(i => i.Price.Receipt));

            var totalSaleOverProvince = currentSales
                .GroupBy(i => i.Invoice.Counterparty!.Person.Region.City)
                .Select(group => new SaleOverRegionDto
                {
                    City = group.Key,
                    SalesPrice = group.Sum(i => i.Price.Receipt),
                    GrowthrRate = CalculateGrowth(
                        group.Sum(i => i.Price.Receipt),
                        previousByCity.TryGetValue(group.Key!, out var prev) ? prev : 0
                    )
                })
                .ToList();

            var totalCurrent = totalSaleOverProvince.Sum(i => i.SalesPrice);
            var totalPrevious = previousByCity.Values.Sum();

            var response = new RegionalSalesDto
            {
                SaleOverRegion = totalSaleOverProvince,
                TotalSale = totalCurrent,
                GrowthrRate = CalculateGrowth(totalCurrent, totalPrevious)
            };
            return ApiResponse<RegionalSalesDto>.Success("Sales Fetched successfully", System.Net.HttpStatusCode.OK, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching sales");
            return ApiResponse<RegionalSalesDto>.Fail("Failed to fetch sales", System.Net.HttpStatusCode.InternalServerError);
        }
        finally
        {
        }
    }



    


}
