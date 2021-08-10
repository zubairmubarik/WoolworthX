using MyDemoProject001.Application.Common.Mappings;
using MyDemoProject001.Domain.Entities;

namespace MyDemoProject001.Application.Common.Models
{
    public class AddressDto : IMapFrom<AddressDocument>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Country { get; set; }     
    }
}
