using BineshSoloution.Enum;
using BineshSoloution.Models.DataBaseModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Models.DataBaseModels.Inventory;

public class Carpet : Product
{
    public string? ColorPalette { get; set; }   
    public string? Density { get; set; }            
    public int ColorCount { get; set; }         
    public string? genus { get; set; } // gens    
    public string? Grade { get; set; }          
    public string? Color { get; set; }          
    public string? BorderColor { get; set; }    
    public string? Size { get; set; }           
    public string? Shoulder { get; set; }       
    public string? WeavePattern { get; set; }   
    public string? DeviceNumber { get; set; }   
    public string? Buyer { get; set; }          
    public string? DesignCode { get; set; }   
    public string? ProjectName { get; set; }    
    public string? DesignName { get; set; }     
    public string? WeaveType { get; set; }      
}
