using BineshSoloution.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BineshSoloution.Models.DataBaseModels.Account;

public class Account
{
    public Guid ID { get; set; }

    public AccountType Type { get; set; }
    public AccountGroupType GroupType { get; set; }
    public AccountGroupTypeDesc GroupTypeDesc { get; set; }

    public bool IsLayerOne { get; set; }
    public string? Name  { get; set; }
    public string? Code { get; set; }
    public Int64 SumDebit { get; set; }
    public Int64 SumCredit { get; set; }
    public Int64 Debit { get; set; }
    public Int64 Credit { get; set; }

    public int inflection { get; set; }
    public int Article { get; set; }
    public DateTime Date { get; set; }
    public DateTime DocDate { get; set; }
    public string? ArticleDescription { get; set; }
    public string? OperationName { get; set; }
    public string? chequeCode { get; set; }
    public string? SeriesNumber { get; set; }
    
    
   // public List<Doc> Docs { get; set; } = default!;
    public List<Account> SubAccounts { get; set; } = default!;

    public Guid? ParentId { get; set; }
    public Account Parent { get; set; } = default!;
}
