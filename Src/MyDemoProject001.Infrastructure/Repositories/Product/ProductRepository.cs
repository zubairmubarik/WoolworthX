using MyDemoProject001.Application.Common.Interfaces;
using MyDemoProject001.Domain.Entities;
using System;
using System.Collections.Generic;

using System.Threading;
using System.Threading.Tasks;

using MongoDB.Driver;

namespace MyDemoProject001.Infrastructure.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        IMongoCollection<ProductDocument> _productsCollection;
        private readonly IMongoContext _mongoContext;
        public ProductRepository(IDatabaseSettings settings, IMongoContext mongoContext)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _productsCollection = database.GetCollection<ProductDocument>("product");

            // Assign the context
            _mongoContext = mongoContext;
        }
        public async Task<ProductDocument> CreateAsync(ProductDocument item, CancellationToken cancellationToken)
        {
            await _productsCollection.InsertOneAsync(item, null, cancellationToken);
            return item;
        }

        public void AddProduct(ProductDocument item, CancellationToken cancellationToken)
        {
            _mongoContext.AddCommandAsync(async () => await _productsCollection.InsertOneAsync(item, null, cancellationToken));
        }

        public Task<long> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDocument>> GetAsync(CancellationToken cancellationToken)
        {
            return (await _productsCollection.FindAsync(productDocument => true, null, cancellationToken)).ToList();
        }

        public Task<ProductDocument> GetAsync(string id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDocument>> GetSortedListAsync(FindOptions<ProductDocument> findOptions, CancellationToken cancellationToken)
        {
            return (await _productsCollection.FindAsync(productDocument => true, findOptions, cancellationToken)).ToList();
        }

        public Task<ProductDocument> PatchAsync(string id, ProductDocument item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDocument> UpdateAsync(string id, ProductDocument item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
