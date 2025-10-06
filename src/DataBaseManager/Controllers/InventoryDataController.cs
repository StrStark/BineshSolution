using DataBaseManager.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Models.DataBaseModels.Inventory;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        [HttpPost("import-json")]
        public async Task<ApiResponse> ImportJson([FromBody] ImportRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.JWT))
                    return ApiResponse.Fail("JWT token is required.", HttpStatusCode.BadRequest);

                if (request.Chunks <= 0)
                    return ApiResponse.Fail("Chunks must be greater than zero.", HttpStatusCode.BadRequest);

                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", request.JWT);

                // for now , we will set this after the first request!
                int InventorySize = request.Chunks;

                for (int page = 1; page <= (InventorySize/request.Chunks) +1; page++)
                {
                    string url = $"http://185.153.211.155:88/api/Anbar/GetKala?pageNumber={page}&pageSize={request.Chunks}";
                    var response = await httpClient.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        return ApiResponse.Fail($"Failed to fetch data from external API. Status: {response.StatusCode}", response.StatusCode);
                    }

                    string content = await response.Content.ReadAsStringAsync();
                    using var jsonDoc = JsonDocument.Parse(content);

                    if (!jsonDoc.RootElement.TryGetProperty("items", out var itemsElement))
                    {
                        return ApiResponse.Fail("JSON response does not contain 'items'.", HttpStatusCode.BadRequest);
                    }

                    if (!jsonDoc.RootElement.TryGetProperty("totalCount", out var InventorySizejson))
                    {
                        return ApiResponse.Fail("JSON response does not contain 'totalCount'.", HttpStatusCode.BadRequest);
                    }

                    InventorySize = JsonSerializer.Deserialize<int>(InventorySizejson.GetRawText());

                    var items = JsonSerializer.Deserialize<List<Item>>(itemsElement.GetRawText());
                    if (items == null || items.Count == 0)
                        continue;

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
                                Shoulder = item.prop5,        // TODO: fix property name
                                Density = item.prop6,         // TODO: implement parser
                                Size = item.prop7,
                                WeaveType = item.prop8,
                                ColorCount = int.TryParse(item.prop9, out var count) ? count : 0,
                                genus = item.prop10,
                                Grade = item.prop11,
                                ProjectName = item.prop12,
                                Manufacturer = item.prop13,
                                ColorPalette = item.prop14,
                                WeavePattern = item.prop15,
                                Buyer = item.prop16,
                                DeviceNumber = item.prop17
                            },
                            "مواد اولیه" => new RawMaterial
                            {

                            },
                            "گلیم" => new Rug
                            {

                            },
                            _ => null!
                        };
                        if (product != null)
                        {
                            _dbContext.Products.Add(product!);
                        }
                    }

                    await _dbContext.SaveChangesAsync();
                }

                return ApiResponse.Success("Data imported successfully.", HttpStatusCode.Accepted);
            }
            catch (JsonException ex)
            {
                return ApiResponse.Fail($"Invalid JSON format: {ex.Message}", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail($"Error importing data: {ex.Message}", HttpStatusCode.InternalServerError);
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
        public class ImportRequest
        {
            public int Chunks { get; set; }
            public string JWT { get; set; }
        }
    }

}
