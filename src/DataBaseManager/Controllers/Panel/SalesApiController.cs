using BineshSoloution.Dtos;
using BineshSoloution.Dtos.Sales;
using BineshSoloution.Interfaces.Account;
using BineshSoloution.Interfaces.Products;
using BineshSoloution.Interfaces.Sales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.UriParser;

using System.Threading;

namespace BineshSoloution.Controllers.Panel;

[ApiController , Route("api/[controller]/[action]")]
public partial class SalesApiController : AppControllerBase
{
    [AutoInject] protected readonly ILogger<SalesApiController> _logger = default!;
    [AutoInject] protected readonly ISalesService _SalesService = default!;
    [AutoInject] protected readonly IAccountService _AccountService = default!;
    [AutoInject] protected readonly IProductService _ProductService = default!;
    [AutoInject] protected readonly AppSettings _AppSettings = default!;

    // you have to implement a service layer later ....
    [HttpPost]
    public async Task<ActionResult<ApiResponse<SalesPageResponsDto>>> GetSalesPageData([FromBody] SalesPageRequestDto request , CancellationToken cancellationToken)
    {
        try
        {
            var duration = request.DateFilter.EndTime - request.DateFilter.StartTime;
            
            var returedAccounts = await _AccountService.GetByNameAsync(_appSettings.ShalliSettings.AccountNames.RestoredItemsAccount, cancellationToken);

            var Sales = await _SalesService.GetByDateDiffrenceAsync(request.DateFilter.StartTime, request.DateFilter.EndTime, cancellationToken);
            var SalesBefor = await _SalesService.GetByDateDiffrenceAsync(request.DateFilter.StartTime - duration, request.DateFilter.StartTime, cancellationToken);

            var returnd = returedAccounts!.SubAccounts.Where(i => i.Date >= (request.DateFilter.StartTime - duration)).ToList();
            var returndbefore = returedAccounts!.SubAccounts.Where(i => i.Date >= (request.DateFilter.StartTime - duration) && i.Date <= request.DateFilter.StartTime).ToList();

            var soldItem = Sales.Select(async p => new SoldItem
            {
                Type =  (await _ProductService.GetByIdAsync(p.ProductId , cancellationToken))!.GetType().ToString(),
                Value = p.Price.Receipt
            }).ToList();

            //needs more thinking.. are we going to solve this in the logic , ir in the mapping ( its better to be solved in the mapping phase ... )
            var returnItem = (await Task.WhenAll(returnd.Select(async p =>
            {
                var num = int.Parse(p.ArticleDescription!.Split(" ")[1]);
                return new ReturnItem
                {
                    Type = (await _appDbContext.Products.Where(p =>
                    p.ProductCode == (_appDbContext.Receipts.Where(R => R.number == num).FirstOrDefault()!.ProductCode)).FirstOrDefaultAsync())?.GetType().ToString()!,
                    Value = p.Debit
                };
            }))).ToList();

            CustomerCategorizedSalesDto test = await _appDbContext.Sales.Where(i => i.Date >= request.DateFilter.StartTime && i.Date <= (request.DateFilter.StartTime + request.DateFilter.TimeFrameUnit switch
            {
                TimeFrameUnit.Day => TimeSpan.FromDays(1),
                TimeFrameUnit.Week => TimeSpan.FromDays(7),
                TimeFrameUnit.Month => TimeSpan.FromDays(30),
                TimeFrameUnit.Year => TimeSpan.FromDays(365),
                _ => TimeSpan.Zero
            })).GroupBy(c =>c.Invoice.); // group them by there category ...


            //var respons = new SalesPageResponsDto
            //{
            //    SalesSummary = new FinancialSummaryDto
            //    {
            //        SoldItems = soldItem,
            //        ReturnItems = returnItem,

            //        Sum = soldItem.Sum(x => x.Value) + returnItem.Sum(x => x.Value),
            //        Count = soldItem.Count + returnItem.Count,

            //        TotalSales = new Card
            //        {
            //            Value = soldItem.Sum(x => x.Value),
            //            Growth = ((soldItem.Sum(x => x.Value) - item_sold_befor.Sum(x => x.Price.Receipt)) / item_sold_befor.Sum(x => x.Price.Receipt))
            //        },
            //        ReturnTotal = new Card
            //        {
            //            Value = returnItem.Sum(x => x.Value),
            //            Growth = ((returnItem.Sum(x => x.Value) - returnd_item_before.Sum(x => x.Debit)) / returnd_item_before.Sum(x => x.Debit))
            //        },
            //        NewModelsSales = new Card
            //        {

            //        },
            //        OffSales = new Card
            //        {

            //        }
            //    },
            //    CategorizedSales = new CategorizedSales // fill this using the quereis ....
            //    {
            //        Sales = null
            //    }
            //};




            return ApiResponse<SalesPageResponsDto>.Success("Sales added successfully", System.Net.HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding sales");
            return ApiResponse<SalesPageResponsDto>.Fail("Failed to add sales", System.Net.HttpStatusCode.InternalServerError);
        }
        finally
        {
        }
    }

    private static double CalculateGrowth(double current, double previous)
    {
        if (previous == 0) return 0;
        return (current - previous) / previous;
    }


}
