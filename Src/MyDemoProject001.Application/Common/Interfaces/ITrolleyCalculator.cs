using MyDemoProject001.Application.Common.Models;

namespace MyDemoProject001.Application.Common.Interfaces
{
    public interface ITrolleyCalculator
    {
        decimal CalculatorAsync(ShoppingListDto item);
    }
}
