using AutoMapper;
using DataBaseManager.DbContexts;
using DataBaseManager.MockData;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Dtos.Inventory;
using Shared.FilteringRequest;
using Shared.Models.DataBaseModels.Account;
using Shared.Models.DataBaseModels.Inventory;
using Shared.Models.DataBaseModels.Sales;
using System.Net;
namespace DataBaseManager.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Test : Controller
    {
      
        private readonly ApplicationDbContext _salesDbContext;
        
        private readonly ILogger<Test> _logger;

        private readonly IMapper _mapper;


        public Test(ApplicationDbContext salesDbContext, ILogger<Test> logger , IMapper mapper)
        {
            _salesDbContext = salesDbContext;
             
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> FillMockAccountingData()
        {
            using var transaction = await _salesDbContext.Database.BeginTransactionAsync();
            try
            {
                if (await _salesDbContext.Accounts.AnyAsync())
                {
                    _logger.LogInformation("Accounts already exist in database. Skipping seeding.");
                    return Ok("Accounts already exist in database.");
                }

                void AttachAccounts(List<Account> accounts)
                {
                    foreach (var acc in accounts)
                    {
                        _salesDbContext.Accounts.Add(acc);

                        if (acc.SubAccounts != null && acc.SubAccounts.Count > 0)
                            AttachAccounts(acc.SubAccounts);
                    }
                }

                AttachAccounts(MockData.MockData.MockAccountData);

                var result = await _salesDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("Mock accounts seeded successfully ({count} rows).", result);
                return Ok($"Mock accounts seeded successfully ({result} rows).");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error seeding mock data.");
                return StatusCode(500, $"Error seeding mock data: {ex.Message}");
            }

        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> FillMockInventoryData()
        {
            await using var transaction = await _salesDbContext.Database.BeginTransactionAsync();
            try
            {
                if (await _salesDbContext.Inventories.AnyAsync())
                {
                    _logger.LogInformation("Inventories already exist in database. Skipping seeding.");
                    return Ok("Inventories already exist in database.");
                }
                foreach (var inventory in MockData.MockData.MockInventoryData)
                {
                    _salesDbContext.Inventories.Add(inventory);
                }

                var result = await _salesDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("Mock inventories seeded successfully ({count} rows).", result);
                return Ok($"Mock inventories seeded successfully ({result} rows).");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error seeding mock inventory data.");
                return StatusCode(500, $"Error seeding mock inventory data: {ex.Message}");
            }

        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<InventoryItemResponseDto?>>> GetInventory()
        {
            using var transaction = await _salesDbContext.Database.BeginTransactionAsync();
            try
            {
                var inventory = await _salesDbContext.Inventories
                                                         .Include(i => i.Products)
                                                         .FirstOrDefaultAsync(i => i.Code == 31);

                if (inventory == null)
                    return NotFound();

                var dto = _mapper.Map<InventoryItemResponseDto>(inventory);

                await transaction.CommitAsync();
                return ApiResponse<InventoryItemResponseDto?>.Success("inventory Found", HttpStatusCode.OK, dto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ApiResponse<InventoryItemResponseDto?>.Fail($"fetch failed... \n message : {ex.Message}" , HttpStatusCode.BadRequest);
            }
        }
    }
}
