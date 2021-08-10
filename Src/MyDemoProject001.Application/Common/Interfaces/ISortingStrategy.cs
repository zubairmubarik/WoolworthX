using MongoDB.Driver;
using MyDemoProject001.Domain.Entities;

namespace MyDemoProject001.Application.Common.Interfaces
{
    public interface ISortingStrategy
    {
        SortDefinition<ProductDocument> Sort();
    }
}
