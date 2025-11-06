using AutoMapper;
using DataBaseManager.Dtos.Inventory;
using DataBaseManager.Models.DataBaseModels.Account;
using DataBaseManager.Models.DataBaseModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager.Mapper;

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
