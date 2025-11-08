using BineshSoloution.Models.DataBaseModels.Sales;
using BineshSoloution.Models.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Models.DataBaseModels.Inventory;

public class Rug : Product 
{
    public string? Name { get; set; }          
    public string? WeaveType { get; set; }     
    public string? Design { get; set; }        
    public string? Color { get; set; }         
    public string? Size { get; set; }          
    public string? Width { get; set; }         
    public string? Buyer { get; set; }         
    public string? DesignCode { get; set; }    
    public int ColorCount { get; set; }       
}
