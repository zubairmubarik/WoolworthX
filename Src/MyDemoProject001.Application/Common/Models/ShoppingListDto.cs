using System.Collections.Generic;

namespace MyDemoProject001.Application.Common.Models
{
    public class ShoppingListDto 
    {
        public List<Product> products { get; set; }
        public List<Special> specials { get; set; }
        public List<Quantity> quantities { get; set; }
    }  
}
