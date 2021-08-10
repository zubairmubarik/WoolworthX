using MyDemoProject001.Domain.Common;

namespace MyDemoProject001.Domain.Entities
{
    public class ProductDocument : AuditableEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}

