namespace BineshSoloution.Dtos.Panel;

public class SalesCardsDto
{
    public Card TotalSales { get; set; } = default!;
    public Card ReturnTotal { get; set; } = default!;
    public Card OffSales { get; set; } = default!;
    public Card NewModelsSales { get; set; } = default!;
}