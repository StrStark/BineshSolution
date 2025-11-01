﻿using Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Customers;

[DtoResourceType(typeof(Models.DataBaseModels.Costumers.Region))]
public class RegionDto
{
    [Required(ErrorMessage = "Region ID is required.")]
    [Display(Name = "Region")]
    public Guid Id { get; set; }

    [Display(Name = "Country")]
    public string? Country { get; set; }

    [Display(Name = "City")]
    public string? City { get; set; }

    [Display(Name = "City Region")]
    public string? CityRegion { get; set; }

    [Display(Name = "Mahale")]
    public string? Mahale { get; set; }
}
