using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.UriParser;
using Shared.Dtos;
using Shared.Dtos.Sales;
using System.Threading;
using WebApplicationApiProvider.Services;

namespace WebApplicationApiProvider.Controller.Sales;

[ApiController , Route("api/[controller]/[action]")]
public partial class SalesApiController : AppControllerBase
{
    [AutoInject] protected readonly ILogger<SalesApiController> _logger = default!;


    // you have to implement a service layer later ....
    [HttpPost]
    public async Task<ActionResult<ApiResponse<SalesPageResponsDto>>> GetAllSales([FromBody] SalesPageRequestDto request)
    {
        try
        {
            var duration = request.DateFilter.EndTime - request.DateFilter.StartTime;

            var sales = _salesController.GetSales(CreateODataQueryForDateDiffrence(request.DateFilter.StartTime, request.DateFilter.EndTime), CancellationToken.None);
            var sales_before = _salesController.GetSales(CreateODataQueryForDateDiffrence(request.DateFilter.StartTime - duration, request.DateFilter.StartTime), CancellationToken.None);

            //var item_sold = await _appDbContext.Sales.Where(i => i.Date >= request.DateFilter.StartTime && i.Date <= request.DateFilter.EndTime).Include(p => p.Price).Include(i => i.Invoice).Include(p => p.Product).ToListAsync();
            //var item_sold_befor = await _appDbContext.Sales.Where(i => i.Date >= (request.DateFilter.StartTime - duration) && i.Date <= request.DateFilter.StartTime).Include(p => p.Price).Include(i => i.Invoice).Include(p => p.Product).ToListAsync();

            //var returnd_item = await _appDbContext.Accounts.Where(i => i.Name == "J برگشت از فروش بازرگانی فرش" && (i.Date >= request.DateFilter.StartTime && i.Date <= request.DateFilter.EndTime)).ToListAsync();
            //var returnd_item_before = await _appDbContext.Accounts.Where(i => i.Name == "J برگشت از فروش بازرگانی فرش" && (i.Date >= (request.DateFilter.StartTime - duration) && i.Date <= request.DateFilter.StartTime)).ToListAsync();

            //var soldItem = item_sold.Select(p => new SoldItem
            //{
            //    Type = p.Product.GetType().ToString(),
            //    Value = p.Price.Receipt
            //}).ToList();
            //var returnItem = (await Task.WhenAll(returnd_item.Select(async p =>
            //{
            //    var num = int.Parse(p.ArticleDescription!.Split(" ")[1]);
            //    return new ReturnItem
            //    {
            //        Type = (await _appDbContext.Products.Where(p =>
            //        p.ProductCode == (_appDbContext.Receipts.Where(R => R.number == num).FirstOrDefault()!.ProductCode)).FirstOrDefaultAsync())?.GetType().ToString()!,
            //        Value = p.Debit
            //    };
            //}))).ToList();

            //CustomerCategorizedSalesDto test = await _appDbContext.Sales.Where(i => i.Date >= request.DateFilter.StartTime && i.Date <= (request.DateFilter.StartTime + request.DateFilter.TimeFrameUnit switch
            //{
            //    TimeFrameUnit.Day => TimeSpan.FromDays(1),
            //    TimeFrameUnit.Week => TimeSpan.FromDays(7),
            //    TimeFrameUnit.Month => TimeSpan.FromDays(30),
            //    TimeFrameUnit.Year => TimeSpan.FromDays(365),
            //    _ => TimeSpan.Zero
            //})).GroupBy(c =>c.Invoice.); // group them by there category ...


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

    private ODataQueryOptions<SalesDto> CreateODataQueryForDateDiffrence(DateTime start, DateTime end)
    {

        return ODataQueryBuilder.FromExpression<SalesDto>(this, x => x.Date >= start && x.Date <= end);
    }

}
