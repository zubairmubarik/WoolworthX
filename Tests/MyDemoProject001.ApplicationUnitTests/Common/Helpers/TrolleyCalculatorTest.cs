using NUnit.Framework;
using MyDemoProject001.Application.Common.Models;
using System.Collections.Generic;

namespace MyDemoProject001.ApplicationUnitTests.Common.Helpers
{
    using static Testing;
    public class TrolleyCalculatorTest : TestBase
    {

        [Test]
        public void ShouldReturn_Zero_Record()
        {
            var shoppingListDto = new ShoppingListDto()
            {
                products = new List<Product>(),
                quantities = new List<Quantity>(),
                specials = new List<Special>()
            };

            decimal expectedResult = 0;

            var gateway = GetITrolleyCalculator();

            var result = gateway.CalculatorAsync(shoppingListDto);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ShouldReturn_NonZero_Record()
        {
            var shoppingListDto = new ShoppingListDto()
            {
                products = new List<Product>()
                {
                    new Product(){ name="1",price= new decimal(2.0)},
                    new Product(){ name="2",price= new decimal(5.0)}
                },
                quantities = new List<Quantity>()
                {   new Quantity(){ name="1",quantity=3 },
                    new Quantity(){ name="2",quantity=2 }
                },
                specials = new List<Special>()
                {
                    new Special(){ quantities = new List<Quantity>(){ new Quantity() { name ="1",quantity=3 } , new Quantity() { name = "2", quantity = 0 } }, total =5 },
                    new Special(){ quantities = new List<Quantity>(){ new Quantity() { name ="1",quantity=1 } , new Quantity() { name = "2", quantity = 2 } }, total =10 }
                }
            };

            decimal expectedResult = 16; //14

            var gateway = GetITrolleyCalculator();

            var result = gateway.CalculatorAsync(shoppingListDto);

            Assert.AreEqual(expectedResult, result);
        }
    }
}