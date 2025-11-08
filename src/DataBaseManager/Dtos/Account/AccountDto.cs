using System.ComponentModel.DataAnnotations;

namespace BineshSoloution.Dtos.Account;

public class AccountDto
{
    [Required(ErrorMessage = "Account ID is required.")]
    [Display(Name = "Account")]
    public Guid Id { get; set; }
    [Display(Name = "Parent Account")]
    public Guid? ParentId { get; set; }
    [Display(Name ="Date")]
    public DateTime Date { get; set; }

    [Display(Name = "Total Debit")]
    public Int64 SumDebit { get; set; }
    [Display(Name = "Total Credit")]
    public Int64 SumCredit { get; set; }
    [Display(Name = "Debit")]
    public Int64 Debit { get; set; }
    [Display(Name = "Credit")]
    public Int64 Credit { get; set; }

    [Display(Name ="Sub Accounts")]
    public List<AccountDto> SubAccounts { get; set; } = default!;
}