using MyDemoProject001.Domain.Common;
using System.Collections.Generic;

namespace MyDemoProject001.Domain.Entities
{
    public class ShopperHistoryDocument : AuditableEntity
    {
        public int CustomerId { get; set; }
        public List<ProductDocument> Products { get; set; }
}
}
