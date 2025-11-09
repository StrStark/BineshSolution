using BineshSoloution.Attributes;
using BineshSoloution.Dtos.Customers;
using BineshSoloution.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Dtos.Sales;

[DtoResourceType(typeof(Models.DataBaseModels.Sales.Invoice))]
public class InvoiceDto
{
    [Required(ErrorMessage = "Invoice ID is required.")]
    [Display(Name = "Invoice")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Invoice type is required.")]
    [Display(Name = "Type")]
    public InvoiceType Type { get; set; }

    [Display(Name = "Is Request")]
    public bool Request { get; set; }

    [Display(Name = "Is Invoice")]
    public bool Invoice { get; set; }

    [Required(ErrorMessage = "Document number is required.")]
    [Display(Name = "Document Number")]
    public int DocNumber { get; set; }

    [Required(ErrorMessage = "Counterparty information is required.")]
    [Display(Name = "Counterparty Id")]
    public Guid CounterpartyId { get; set; } = default!;
    [Display(Name = "Counterparty")]
    public CustomerDto? Counterparty { get; set; }
}
