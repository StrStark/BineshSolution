using Shared.Models.DataBaseModels.Account;
using Shared.Models.DataBaseModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Inventory;

public class InventoryItemResponseDto
{
    public Guid Id { get; set; }

    public int Code { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? Manager { get; set; }

    public Account? Account { get; set; } = default!;

    public List<Carpet> Carpets { get; set; } = default!;
    public List<Rug> Rugs { get; set; } = default!;
    public List<RawMaterial> RawMaterials { get; set; } = default!;
}
