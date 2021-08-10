using MongoDB.Driver;
using MyDemoProject001.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyDemoProject001.Application.Common.Interfaces
{
    public interface IProductRepository : IGenericInterface<ProductDocument>
    {
        Task<List<ProductDocument>> GetSortedListAsync(FindOptions<ProductDocument> findOptions, CancellationToken cancellationToken);
        void AddProduct(ProductDocument item, CancellationToken cancellationToken);
    }
}
