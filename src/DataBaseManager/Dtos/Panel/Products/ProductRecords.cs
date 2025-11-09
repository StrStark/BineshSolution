namespace BineshSoloution.Dtos.Panel.Products;

public class ProductRecords
{
    public string Identifire { get; set; } = default!;
    public string? ProductState { get; set; } = default!; // if its sales record , then the date should be used as sales date, and if its byu use it as buy date .... 
    public DateTime RecordDate { get; set; } = default!;
}
