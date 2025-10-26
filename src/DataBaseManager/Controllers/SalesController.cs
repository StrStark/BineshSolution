using AutoMapper;
using DataBaseManager.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Dtos.Sales;
using Shared.Models.DataBaseModels.Sales;

namespace DataBaseManager.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SalesController : Controller
    {
        private readonly AccountingDbContext _AccountingDbContext;
        private readonly CustomerDbContext _CustomerDbContext;
        private readonly InventoryDbContext _inventoryDbContext;
        private readonly SalesDbContext _salesDbContext;

        private readonly ILogger<Test> _logger;

        private readonly IMapper _mapper;


        public SalesController(AccountingDbContext AccountingDbContext, CustomerDbContext CustomerDbContext, InventoryDbContext inventoryDbContext, SalesDbContext salesDbContext, ILogger<Test> logger, IMapper mapper)
        {
            _AccountingDbContext = AccountingDbContext;
            _CustomerDbContext = CustomerDbContext;
            _salesDbContext = salesDbContext;
            _inventoryDbContext = inventoryDbContext;

            _logger = logger;
            _mapper = mapper;

        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddSales([FromBody] SalesRecordAddingRequestDto request) // needs removing...
        {
            var transition = await _salesDbContext.Database.BeginTransactionAsync();
            try
                {

                var product = await _inventoryDbContext.Products
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
                await _salesDbContext.Sales.AddAsync(sale);
                var resualt = await _salesDbContext.SaveChangesAsync();

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
            var transition = await _salesDbContext.Database.BeginTransactionAsync();
            try
            {
                var item_sold = await _salesDbContext.Sales.Where(i => i.Date >= request.DateFilter.StartTime && i.Date <= request.DateFilter.EndTime).ToListAsync();

                var respons = new SalesPageResponsDto
                {
                    SalesSummary = new FinancialSummaryDto
                    {
                        SoldItems= new List<SoldItem>
                        {

                        }
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
