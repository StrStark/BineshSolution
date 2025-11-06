using MassTransit;
using Shared.Dtos.Sales;

namespace WebApplicationApiProvider.Consumer;

public partial class SaleConsumer : IConsumer<SalesDto>
{
    [AutoInject] private readonly ILogger<SaleConsumer> _logger = default!;

    public async Task Consume(ConsumeContext<SalesDto> context)
    {
        var sales = context.Message;
        _logger.LogInformation("📦 Received SalesDto: " +
            $"Id={sales.Id}, " +
            $"Product={sales.ProductId}, " +
            $"CarpetId={sales.Carpet?.Id}, " +
            $"RugId={sales.Rug?.Id}, " +
            $"RawMaterialId={sales.RawMaterial?.Id}, " +
            $"InvoiceId={sales.Invoice?.Id}, " +
            $"PriceId={sales.Price?.Id}, " +
            $"DeliveredQuantity={sales.DeliveredQuantity}, " +
            $"Date={sales.Date}");


        // Example: You can call your own APIs, trigger workflows, or update cache here
        await Task.CompletedTask;
    }
}
