using AutoMapper;
using BineshSoloution.Dtos.Inventory;
using BineshSoloution.Models.Account;
using BineshSoloution.Models.DataBaseModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BineshSoloution.Mapper;

public class InventoryMappingProfile : Profile
{
    public InventoryMappingProfile()
    {
        CreateMap<Inventory, InventoryItemResponseDto>()
            .ForMember(dest => dest.Carpets,
                opt => opt.MapFrom(src => src.Products.OfType<Carpet>().ToList()))
            .ForMember(dest => dest.Rugs,
                opt => opt.MapFrom(src => src.Products.OfType<Rug>().ToList()))
            .ForMember(dest => dest.RawMaterials,
                opt => opt.MapFrom(src => src.Products.OfType<RawMaterial>().ToList()));

        //map the rest!
        CreateMap<Account, Account>();

        CreateMap<Carpet, Carpet>();
        CreateMap<Rug, Rug>();
        CreateMap<RawMaterial, RawMaterial>();
    }
}
