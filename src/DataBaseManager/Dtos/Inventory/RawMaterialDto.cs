using BineshSoloution.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Dtos.Inventory;

[DtoResourceType(typeof(Models.DataBaseModels.Inventory.RawMaterial))]
public class RawMaterialDto : ProductDto
{
    [Display(Name = "Type")]
    public string? Type { get; set; }

    [Display(Name = "Gender")]
    public string? Gender { get; set; }

    [Display(Name = "Package Type")]
    public string? PackageType { get; set; }

    [Display(Name = "Color")]
    public string? Color { get; set; }

    [Display(Name = "Number")]
    public string? Number { get; set; }

    [Display(Name = "Serial")]
    public string? Serial { get; set; }

    [Display(Name = "Code")]
    public string? Code { get; set; }

    [Display(Name = "Device Usage")]
    public string? DeviceUsage { get; set; }

    [Display(Name = "Project Name")]
    public string? ProjectName { get; set; }

    [Display(Name = "Extra Field 1")]
    public string? Extra1 { get; set; }

    [Display(Name = "Extra Field 2")]
    public string? Extra2 { get; set; }

    [Display(Name = "Extra Field 3")]
    public string? Extra3 { get; set; }
}
