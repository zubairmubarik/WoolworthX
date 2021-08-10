using AutoMapper;
using MyDemoProject001.Application.Common.Mappings;
using MyDemoProject001.Domain.Entities;
using Newtonsoft.Json;

namespace MyDemoProject001.Application.Common.Models
{
    public class CustomerDto : IMapFrom<CustomerDocument>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }             

        public AddressDto Address { get; set; }

        public string JsonResponded { get; set; }
        public void Mapping(Profile profile)
        {            
            profile.CreateMap<CustomerDocument, CustomerDto>().ForMember(x=>x.JsonResponded,opt=>opt.MapFrom(c=>JsonConvert.SerializeObject(c)));
        }

    }
}
