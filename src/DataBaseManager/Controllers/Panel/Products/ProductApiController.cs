using BineshSoloution.Dtos;
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
    public async Task<ActionResult<ApiResponse<ProductCardDto>>> GetProductCardsAsync([FromBody] ProductPageRequestDto request , CancellationToken cancellationToken)
    {
        try
        {
            var Product = _ProductService.GetByInventoryIdAndCategoryAsync(request.InventoryDto.InventoryCode, request.CategoryDto.ProductCategory , cancellationToken);

            var response = new ProductCardDto
            {
                 // finish it later ..
            };

            return ApiResponse<ProductCardDto>.Success("Sales Fetched successfully", System.Net.HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching product cards");
            return ApiResponse<ProductCardDto>.Fail("Failed to fetch products", System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
