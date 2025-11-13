using BineshSoloution.Dtos;
using BineshSoloution.Dtos.Panel;
using BineshSoloution.Dtos.Panel.Products;
using BineshSoloution.Dtos.Panel.Sales;
using Microsoft.AspNetCore.Mvc;

namespace BineshSoloution.Controllers.Panel.Products;


// you have to implement a service layer later ....
[ApiController, Route("api/[controller]/[action]")]
public partial class ProductApiController : AppControllerBase
{
    [AutoInject] protected readonly ILogger<SalesApiController> _logger = default!;

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductCardDto?>>> GetProductCardsAsync([FromBody] ProductPageRequestDto request , CancellationToken cancellationToken)
    {
        try
        {
            var Product = await  _ProductService.GetByInventoryIdAndCategoryAsync(request.InventoryDto.InventoryCode, request.CategoryDto.ProductCategory , cancellationToken);
           
            var response = new ProductCardDto
            {
                 TotalProdact = new Card
                 {
                     Value = Product!.Count,
                     Growth = CalculateGrowth(Product.Count, 100) // how to determine it ...
                 },
                 InventoryCirculationCount = new Card
                 {
                     Value = 2, // how to determine it ...
                     Growth = CalculateGrowth(100, 100) // how to determine it ...
                 }, 
                 InventoryCirculationDays = new Card
                 {
                     Value = 180, // how to determine it ...
                     Growth = CalculateGrowth(100, 100) // how to determine it ...
                 }
            };

            return ApiResponse<ProductCardDto?>.Success("Sales Fetched successfully", System.Net.HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching product cards");
            return ApiResponse<ProductCardDto?>.Fail("Failed to fetch products", System.Net.HttpStatusCode.InternalServerError);
        }
    }

    //records for this page is inside the sales and inventory .... 
    //its better to use havale and resid for this section ...
    // i will stop the development here for a while ...
    [HttpPost]
    public async Task<ActionResult<ApiResponse<List<ProductItem>?>>> GetProducts([FromBody] ProductPageRequestDto request , CancellationToken cancellationToken)
    {
        try
        {
            var products = await  _ProductService.GetByInventoryIdAndCategoryAsync(request.InventoryDto.InventoryCode, request.CategoryDto.ProductCategory , cancellationToken);


            var response = new List<ProductItem>()
            {

            };
            return ApiResponse<List<ProductItem>?>.Success("Products Fetched successfully", System.Net.HttpStatusCode.OK, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching products");
            return ApiResponse<List<ProductItem>?>.Fail("Failed to fetch products", System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
