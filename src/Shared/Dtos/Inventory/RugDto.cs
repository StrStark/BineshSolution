using Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Inventory;

[DtoResourceType(typeof(Models.DataBaseModels.Inventory.Rug))]
public class RugDto : ProductDto
{
    [Display(Name = "Name")]
    public string? Name { get; set; }

    [Display(Name = "Weave Type")]
    public string? WeaveType { get; set; }

    [Display(Name = "Design")]
    public string? Design { get; set; }

    [Display(Name = "Color")]
    public string? Color { get; set; }

    [Display(Name = "Size")]
    public string? Size { get; set; }

    [Display(Name = "Width")]
    public string? Width { get; set; }

    [Display(Name = "Buyer")]
    public string? Buyer { get; set; }

    [Display(Name = "Design Code")]
    public string? DesignCode { get; set; }

    [Display(Name = "Color Count")]
    public int ColorCount { get; set; }
}
