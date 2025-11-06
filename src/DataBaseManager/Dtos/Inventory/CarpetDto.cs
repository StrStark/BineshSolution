using DataBaseManager.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager.Dtos.Inventory;

[DtoResourceType(typeof(Models.DataBaseModels.Inventory.Carpet))]
public class CarpetDto : ProductDto
{
    [Display(Name = "Color Palette")]
    public string? ColorPalette { get; set; }

    [Display(Name = "Density")]
    public string? Density { get; set; }

    [Display(Name = "Color Count")]
    public int ColorCount { get; set; }

    [Display(Name = "Genus")]
    public string? Genus { get; set; }

    [Display(Name = "Grade")]
    public string? Grade { get; set; }

    [Display(Name = "Color")]
    public string? Color { get; set; }

    [Display(Name = "Border Color")]
    public string? BorderColor { get; set; }

    [Display(Name = "Size")]
    public string? Size { get; set; }

    [Display(Name = "Shoulder")]
    public string? Shoulder { get; set; }

    [Display(Name = "Weave Pattern")]
    public string? WeavePattern { get; set; }

    [Display(Name = "Device Number")]
    public string? DeviceNumber { get; set; }

    [Display(Name = "Buyer")]
    public string? Buyer { get; set; }

    [Display(Name = "Design Code")]
    public string? DesignCode { get; set; }

    [Display(Name = "Project Name")]
    public string? ProjectName { get; set; }

    [Display(Name = "Design Name")]
    public string? DesignName { get; set; }

    [Display(Name = "Weave Type")]
    public string? WeaveType { get; set; }
}
