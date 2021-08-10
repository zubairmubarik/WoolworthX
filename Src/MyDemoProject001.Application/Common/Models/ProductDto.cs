using AutoMapper;
using MyDemoProject001.Application.Common.Mappings;
using MyDemoProject001.Domain.Entities;

namespace MyDemoProject001.Application.Common.Models
{
    public class ProductDto : IMapFrom<ProductDocument>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductDocument, ProductDto>();            
        }
    }
}