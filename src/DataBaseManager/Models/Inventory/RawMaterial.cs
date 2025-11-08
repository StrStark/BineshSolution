using BineshSoloution.Models.DataBaseModels.Sales;
using BineshSoloution.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Models.DataBaseModels.Inventory;

public class RawMaterial : Product
{
    public string? Type { get; set; }          
    public string? Gender { get; set; }        
    public string? PackageType { get; set; }   
    public string? Color { get; set; }         
    public string? Number { get; set; }        
    public string? Serial { get; set; }        
    public string? Code { get; set; }          
    public string? DeviceUsage { get; set; }   
    public string? ProjectName { get; set; }   
    public string? Extra1 { get; set; }        
    public string? Extra2 { get; set; }        
    public string? Extra3 { get; set; }        
}
