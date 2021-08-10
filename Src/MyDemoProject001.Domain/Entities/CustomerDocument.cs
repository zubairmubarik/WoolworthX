using MyDemoProject001.Domain.Common;

namespace MyDemoProject001.Domain.Entities
{
    public class CustomerDocument : AuditableEntity
    {           
        public string  Name { get; set; }
                
        public string Email { get; set; }   

        public AddressDocument Address { get; set; }
    }
}
