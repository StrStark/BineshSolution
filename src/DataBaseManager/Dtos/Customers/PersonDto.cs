using DataBaseManager.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager.Dtos.Customers;

[DtoResourceType(typeof(Models.DataBaseModels.Costumers.Person))]
public class PersonDto
{
    [Required(ErrorMessage = "Person ID is required.")]
    [Display(Name = "Person")]
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    public string? Name { get; set; }

    [Display(Name = "Family")]
    public string? Family { get; set; }

    [Display(Name = "Phone")]
    public string? Phone { get; set; }

    [Display(Name = "Fax")]
    public string? Fax { get; set; }

    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Region information is required.")]
    [Display(Name = "Region")]
    public RegionDto Region { get; set; } = default!;
}
