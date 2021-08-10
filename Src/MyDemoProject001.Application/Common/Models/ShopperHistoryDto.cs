using AutoMapper;
using MyDemoProject001.Application.Common.Mappings;
using MyDemoProject001.Domain.Entities;
using System.Collections.Generic;

namespace MyDemoProject001.Application.Common.Models
{
    public class ShopperHistoryDto : IMapFrom<ShopperHistoryDocument> 
    {
        public int CustomerId { get; set; }
        public List<ProductDto> Products { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShopperHistoryDocument, ShopperHistoryDto>()
                .ForMember(sh => sh.Products, pro => pro.MapFrom(p => p.Products));            
        }

    }
}

