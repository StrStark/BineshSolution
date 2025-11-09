namespace BineshSoloution.Models.DataBaseModels.Costumers;

public class Region
{
    public Guid Id { get; set; }

    public string? Country { get; set; }
    public string? Province { get; set; }
    public string? City { get; set; }
}