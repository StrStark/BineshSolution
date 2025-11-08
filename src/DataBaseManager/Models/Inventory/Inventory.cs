using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BineshSoloution.Models.DataBaseModels.Account;


namespace BineshSoloution.Models.DataBaseModels.Inventory;

public class Inventory
{
    public Guid Id { get; set; }

    public int Code { get; set; }    
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? Manager { get; set; }

    public Guid? AccountId { get; set; }
    public Account.Account? Account { get; set; } = default!;
    
    public bool BarcodeEnabled { get; set; }
    public bool Identifiable { get; set; }
    public bool IsMonetary { get; set; }
    public bool AllowNegativeStock { get; set; }
    public bool UseAverageOrFIFOFromYearStart { get; set; }
    public bool ActiveInReceiptRegistration { get; set; }
    public bool ActiveInReports { get; set; }
    public bool LimitByAccountAndDocumentType { get; set; }
    public bool LimitByMarketer { get; set; }
    public bool AffectStockCalculation { get; set; }
    public bool AffectProductionRequestBySalesOrder { get; set; }

    public List<Product> Products { get; set; } = default!;


}
