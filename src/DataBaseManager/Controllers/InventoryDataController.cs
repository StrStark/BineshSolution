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
                                InventoryCode = item.kCode,
                                InventoryDesc = item.kDesc,
                                InventoryDesc2 = item.kDesc2,
                                InventoryDescBarcode = item.kDescBarcode,
                                InventoryDescLatin = item.kDescLatin,
                                InventoryIsActive = item.kIsActive,
                                DesignName = item.prop1,
                                Color = item.prop2,
                                BorderColor = item.prop3,
                                DesignCode = item.prop4,
                                Shoulder = item.prop5,//fix the damn name
                                Density = item.prop6,// implement parser
                                Size = item.prop7,
                                WeaveType = item.prop8,
                                ColorCount = int.Parse(item.prop9),
                                genus = item.prop10,
                                Grade = item.prop11,
                                ProjectName = item.prop12,
                                Manufacturer = item.prop13,
                                ColorPalette = item.prop14,
                                WeavePattern = item.prop15,
                                Buyer = item.prop16,
                                DeviceNumber = item.prop17,
                                

                            },
                            "مواد اولیه" => new RawMaterial
                            {
                                InventoryCode = item.kCode,
                                InventoryDesc = item.kDesc,
                                InventoryDesc2 = item.kDesc2,
                                InventoryDescBarcode = item.kDescBarcode,
                                InventoryDescLatin = item.kDescLatin,
                                InventoryIsActive = item.kIsActive,
                            },
                            "" => new Rug
                            {
                                InventoryCode = item.kCode,
                                InventoryDesc = item.kDesc,
                                InventoryDesc2 = item.kDesc2,
                                InventoryDescBarcode = item.kDescBarcode,
                                InventoryDescLatin = item.kDescLatin,
                                InventoryIsActive = item.kIsActive,
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
