using MyDemoProject001.Application.Common.Interfaces;
using MyDemoProject001.Application.Common.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Collections;

namespace MyDemoProject001.Application.Common.Helpers
{
    public class TrolleyCalculator : ITrolleyCalculator
    {
        public decimal CalculatorAsync(ShoppingListDto shoppingList)
        {
            decimal _total = 0;

            #region Validation

            if (shoppingList.products.Count == 0)
                return _total;

            var products = (from p in shoppingList.products
                            join q in shoppingList.quantities on p.name equals q.name
                            where q.quantity > 0
                            select new ProductDto { Name = p.name, Price = p.price, Quantity = q.quantity }).ToList();

            if (products.Count == 0)
                return _total;

            var specialBuys = shoppingList.specials;

            #endregion

            if (specialBuys.Count > 0)
            {
                #region Buy from Special

                Hashtable hashtableForSpecials = LoadHashtableForSpecials(products, specialBuys);
                Hashtable hashtableForProductsPrice = LoadHashtableForBestPrice(products);

                foreach (var product in products.Where(x => x.Quantity > 0))
                {
                    var specialProducts = (List<Special>)hashtableForSpecials[product.Name];
                    if (specialProducts == null)
                        continue;

                    foreach (var special in specialProducts)
                    {
                        if (special.quantities.Any(x => x.quantity == 0))
                            continue;

                        var productSpecialQuantity = special.quantities.FirstOrDefault(x => x.name == product.Name);

                        if (productSpecialQuantity == null)
                            continue;


                        var comparedPrice = Decimal.Compare(special.total, productSpecialQuantity.quantity * product.Price);
                        if (comparedPrice <= 0)
                        {
                            _total = _total + special.total;
                            product.Quantity = product.Quantity - productSpecialQuantity.quantity;                            
                        }                        
                    }
                }

                #endregion

            }

            #region Calculate remaining product items
            foreach (var p in products)
            {
                for (; p.Quantity > 0; p.Quantity--)
                    _total = _total + (p.Price * 1);
            }
            #endregion

            return _total;
        }

        private Hashtable LoadHashtableForBestPrice(List<ProductDto> products)
        {
            Hashtable hashtableForBestPrice = new Hashtable();

            foreach (var product in products)
                hashtableForBestPrice.Add(product.Name, product.Price * product.Quantity);

            return hashtableForBestPrice;
        }
        private Hashtable LoadHashtableForSpecials(List<ProductDto> products, List<Special> specialBuys)
        {
            Hashtable hashtableForSpecials = new Hashtable();
            foreach (var product in products)
            {
                var specialBuysForListedProducts = specialBuys.Where(s => s.quantities.Where(q => q.name == product.Name && q.quantity > 0).Any() == true).Select(s => s).ToList();

                if (specialBuysForListedProducts != null)
                    hashtableForSpecials.Add(product.Name, specialBuysForListedProducts);
            }
            return hashtableForSpecials;
        }
    }
}