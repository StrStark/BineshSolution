namespace Shared.Models.DataBaseModels.Account;

public class Doc
{
    public Guid Id { get; set; }

    public int Row { get; set; }
    public int arrangement { get; set; }
    public string? AccountName { get; set; }
    public string? ArticleDesciption { get; set; }
    public DateTime OperationDate { get; set; }
    public Int64 Debit { get; set; }
    public Int64 Credit { get; set; }
    public string? Operation { get; set; }

    public Guid AccountId { get; set; }
    public Account Account { get; set; } = default!;
}