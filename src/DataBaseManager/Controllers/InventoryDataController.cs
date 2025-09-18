using DataBaseManager.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Models.DataBaseModels.Inventory;
using System.Net;
using System.Text.Json;

namespace DataBaseManager.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InventoryDataController : Controller
    {
        private readonly InventoryDbContext _dbContext;

        public InventoryDataController(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<ApiResponse> ImportJson([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return ApiResponse.Success("No file uploaded.", HttpStatusCode.BadRequest);

            try
            {
                using var stream = file.OpenReadStream();
                var jsonDoc = await JsonDocument.ParseAsync(stream);

                if (!jsonDoc.RootElement.TryGetProperty("items", out var itemsElement))
                    return ApiResponse.Success("JSON does not contain 'items'.", HttpStatusCode.BadRequest);

                var items = JsonSerializer.Deserialize<List<Item>>(itemsElement.GetRawText());

                if (items != null)
                {
                    foreach (var item in items)
                    {
                        Product product = item.grp switch
                        {
                            "فرش" => new Carpet
                            {
                                
                            },
                            "مواد اولیه" => new RawMaterial
                            {

                            },
                            "" => new Rug
                            {

                            },
                            _ => throw new NotImplementedException()
                        };
                        _dbContext.Products.Add(product);
                    }

                    await _dbContext.SaveChangesAsync();
                }

                return new ApiResponse { Success = true, Message = "Data imported successfully." };
            }
            catch (Exception ex)
            {
                return new ApiResponse { Success = false, Message = $"Error importing data: {ex.Message}" };
            }
        }
        public record Item(
            string kCode,
            string kDesc,
            string? kDesc2,
            string? kDescBarcode,
            string? kDescLatin,
            bool kIsActive,
            string grp,
            string prop1,
            string prop2,
            string prop3,
            string prop4,
            string prop5,
            string prop6,
            string prop7,
            string prop8,
            string prop9,
            string prop10,
            string prop11,
            string prop12,
            string prop13,
            string prop14,
            string prop15,
            string prop16,
            string prop17
        );

    }

}
