using DataBaseManager.DbContexts;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Models.DataBaseModels.Account;
using DataBaseManager.MockData;
using DocumentFormat.OpenXml.Bibliography;
using Shared.Models.DataBaseModels.Inventory;
namespace DataBaseManager.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Test : Controller
    {
        private readonly AccountingDbContext _AccountingDbContext;
        private readonly CustomerDbContext _CustomerDbContext;
        private readonly InventoryDbContext _inventoryDbContext;
        private readonly SalesDbContext _salesDbContext;
        private readonly ILogger<Test> _logger;

        public Test(AccountingDbContext AccountingDbContext, CustomerDbContext CustomerDbContext, InventoryDbContext inventoryDbContext, SalesDbContext salesDbContext, ILogger<Test> logger)
        {
            _AccountingDbContext = AccountingDbContext;
            _CustomerDbContext = CustomerDbContext;
            _salesDbContext = salesDbContext;
            _inventoryDbContext = inventoryDbContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> FillMockAccountingData()
        {
            using var transaction = await _AccountingDbContext.Database.BeginTransactionAsync();
            try
            {
                if (await _AccountingDbContext.Accounts.AnyAsync())
                {
                    _logger.LogInformation("Accounts already exist in database. Skipping seeding.");
                    return Ok("Accounts already exist in database.");
                }

                void AttachAccounts(List<Account> accounts)
                {
                    foreach (var acc in accounts)
                    {
                        _AccountingDbContext.Accounts.Add(acc);

                        if (acc.SubAccounts != null && acc.SubAccounts.Count > 0)
                            AttachAccounts(acc.SubAccounts);
                    }
                }

                AttachAccounts(MockData.MockData.MockAccountData);

                var result = await _AccountingDbContext.SaveChangesAsync();
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
            await using var transaction = await _inventoryDbContext.Database.BeginTransactionAsync();
            try
            {
                if (await _inventoryDbContext.Inventories.AnyAsync())
                {
                    _logger.LogInformation("Inventories already exist in database. Skipping seeding.");
                    return Ok("Inventories already exist in database.");
                }
                foreach (var inventory in MockData.MockData.MockInventoryData)
                {
                    _inventoryDbContext.Inventories.Add(inventory);
                }

                var result = await _inventoryDbContext.SaveChangesAsync();
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
    }
}
