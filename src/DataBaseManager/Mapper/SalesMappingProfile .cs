using AutoMapper;
using DataBaseManager.Dtos.Inventory;
using DataBaseManager.Dtos.Sales;
using DataBaseManager.Models.DataBaseModels.Inventory;
using DataBaseManager.Models.DataBaseModels.Sales;
using DataBaseManager.Models.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseManager.Mapper;


public class SalesMappingProfile : Profile
{
    public SalesMappingProfile()
    {
        
        CreateMap<Carpet, CarpetDto>();
        CreateMap<Rug, RugDto>();
        CreateMap<RawMaterial, RawMaterialDto>();

        CreateMap<CarpetDto, Carpet>()
            .ForMember(dest => dest.Inventory, opt => opt.Ignore())
            .ForMember(dest => dest.EnteryDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EnteryDate, DateTimeKind.Utc)))
            .ForMember(dest => dest.ExitDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.ExitDate, DateTimeKind.Utc)));

        CreateMap<RugDto, Rug>()
                .ForMember(dest => dest.Inventory, opt => opt.Ignore())
                .ForMember(dest => dest.EnteryDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EnteryDate, DateTimeKind.Utc)))
                .ForMember(dest => dest.ExitDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.ExitDate, DateTimeKind.Utc)));

        CreateMap<RawMaterialDto, RawMaterial>()
                .ForMember(dest => dest.Inventory, opt => opt.Ignore())
                .ForMember(dest => dest.EnteryDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EnteryDate, DateTimeKind.Utc)))
                .ForMember(dest => dest.ExitDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.ExitDate, DateTimeKind.Utc)));



        CreateMap<Invoice, InvoiceDto>();
        CreateMap<InvoiceDto, Invoice>();
        CreateMap<Price, PriceDto>();
        CreateMap<PriceDto, Price>();

        CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.EnteryDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EnteryDate, DateTimeKind.Utc)))
               .ForMember(dest => dest.ExitDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.ExitDate, DateTimeKind.Utc)));

        CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.EnteryDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EnteryDate, DateTimeKind.Utc)))
               .ForMember(dest => dest.ExitDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.ExitDate, DateTimeKind.Utc)));

        CreateMap<Sales, SalesDto>()
            .ForMember(dest => dest.Carpet,
                opt => opt.MapFrom(src => src.Product is Carpet ? (Carpet)src.Product : null))
            .ForMember(dest => dest.Rug,
                opt => opt.MapFrom(src => src.Product is Rug ? (Rug)src.Product : null))
            .ForMember(dest => dest.RawMaterial,
                opt => opt.MapFrom(src => src.Product is RawMaterial ? (RawMaterial)src.Product : null))
            .ForMember(dest => dest.Invoice, opt => opt.MapFrom(src => src.Invoice))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Date, DateTimeKind.Utc)))
            .ForMember(dest => dest.DeliveredQuantity, opt => opt.MapFrom(src => src.DeliveredQuantity));

        
        CreateMap<SalesDto, Sales>()
            .ForMember(dest => dest.Invoice, opt => opt.MapFrom(src => src.Invoice))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Date, DateTimeKind.Utc)))
            .ForMember(dest => dest.DeliveredQuantity, opt => opt.MapFrom(src => src.DeliveredQuantity))
            .AfterMap((src, dest, ctx) =>
            {
                if (src.Carpet != null) dest.Product = ctx.Mapper.Map<Carpet>(src.Carpet);
                else if (src.Rug != null) dest.Product = ctx.Mapper.Map<Rug>(src.Rug);
                else if (src.RawMaterial != null) dest.Product = ctx.Mapper.Map<RawMaterial>(src.RawMaterial);
                else dest.Product = null; 
            });
    }
}