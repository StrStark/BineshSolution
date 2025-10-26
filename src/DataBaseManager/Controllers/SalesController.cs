using AutoMapper;
using DataBaseManager.DbContexts;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Dtos.Sales;
using Shared.Models.DataBaseModels.Account;
using Shared.Models.DataBaseModels.Sales;

namespace DataBaseManager.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SalesController : Controller
    {
       
        private readonly ApplicationDbContext _appDbContext;

        private readonly ILogger<Test> _logger;

        private readonly IMapper _mapper;


        public SalesController(ApplicationDbContext appDbContext, ILogger<Test> logger, IMapper mapper)
        {

            _appDbContext = appDbContext;

            _logger = logger;
            _mapper = mapper;

        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddSales([FromBody] SalesRecordAddingRequestDto request) // needs removing...
        {
            var transition = await _appDbContext.Database.BeginTransactionAsync();
            try
                {

                var product = await _appDbContext.Products
                .FirstOrDefaultAsync(p => p.Id == request.ProductId);

                if (product == null)
                    return ApiResponse.Fail("Product not found", System.Net.HttpStatusCode.NotFound);


                var sale = new Sales
                {
                    ProductId = request.ProductId,

                    Invoice = new Invoice
                    {
                        Type = request.InvoiceType,
                        Request = request.InvoiceRequest,
                        invoice = request.InvoiceFlag,
                        DocNumber = request.DocNumber,
                       // Counterparty = request.Counterparty
                    },

                    Price = new Price
                    {
                        Fee = request.PriceFee,
                        Receipt = request.PriceReceipt,
                        Voucher = request.PriceVoucher
                    },

                    State = request.State,
                    Date = request.Date,
                    Incoming = request.Incoming,
                    Outgoing = request.Outgoing,
                    RequestNumber = request.RequestNumber,
                    DeliveredQuantity = request.DeliveredQuantity
                };
                await _appDbContext.Sales.AddAsync(sale);
                var resualt = await _appDbContext.SaveChangesAsync();

                await transition.CommitAsync();
                return ApiResponse.Success("Sales added successfully", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await transition.RollbackAsync();
                _logger.LogError(ex, "Error adding sales");
                return ApiResponse.Fail("Failed to add sales", System.Net.HttpStatusCode.InternalServerError);
            }
            finally
            {
                await transition.DisposeAsync();
            }
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<List<SalesPageResponsDto>>>> GetAllSales([FromBody] SalesPageRequestDto request) 
        { 
            var transition = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                var duration = request.DateFilter.EndTime - request.DateFilter.StartTime;
                
                
                var item_sold = await _appDbContext.Sales.Where(i => i.Date >= request.DateFilter.StartTime && i.Date <= request.DateFilter.EndTime).Include(p=> p.Price).Include(i=>i.Invoice).Include(p=>p.Product).ToListAsync();
                var item_sold_befor = await _appDbContext.Sales.Where(i => i.Date >= (request.DateFilter.StartTime - duration) && i.Date <= request.DateFilter.StartTime).Include(p => p.Price).Include(i => i.Invoice).Include(p => p.Product).ToListAsync();
                
                
                var returnd_item = await _appDbContext.Accounts.Where(i=> i.Name == "J برگشت از فروش بازرگانی فرش" && (i.Date>=request.DateFilter.StartTime && i.Date<=request.DateFilter.EndTime) ).ToListAsync();
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

                var respons = new SalesPageResponsDto
                {
                    SalesSummary = new FinancialSummaryDto
                    {
                        SoldItems = soldItem,
                        ReturnItems = returnItem,
                        TotalSales = new Card
                        {
                            Value = soldItem.Sum(x=>x.Value),
                            Growth = ((soldItem.Sum(x=>x.Value) - item_sold_befor.Sum(x=>x.Price.Receipt)) / item_sold_befor.Sum(x => x.Price.Receipt))
                        }
                        //......
                    }
                };




                await transition.CommitAsync();
                return ApiResponse<List<SalesPageResponsDto>>.Success("Sales added successfully", System.Net.HttpStatusCode.OK );
            }
            catch (Exception ex)
            {
                await transition.RollbackAsync();
                _logger.LogError(ex, "Error adding sales");
                return ApiResponse<List<SalesPageResponsDto>>.Fail("Failed to add sales", System.Net.HttpStatusCode.InternalServerError);
            }
            finally
            {
                await transition.DisposeAsync();
            }
        }
    }
}
