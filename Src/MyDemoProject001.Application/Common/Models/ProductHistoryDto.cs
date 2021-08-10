using AutoMapper;
using MyDemoProject001.Application.Common.Mappings;
using MyDemoProject001.Domain.Entities;
using System.Collections.Generic;

namespace MyDemoProject001.Application.Common.Models
{
    public class ProductHistoryDto : IMapFrom<ShopperHistoryDocument>
    {        
        public List<ProductDto> ProductDto { get; set; }
        public void Mapping(Profile profile)
        {   
            profile.CreateMap<ShopperHistoryDocument, ProductHistoryDto>()
                .ForMember(dest => dest.ProductDto, opt => opt.MapFrom(src => src.Products));
        }
    }
}
