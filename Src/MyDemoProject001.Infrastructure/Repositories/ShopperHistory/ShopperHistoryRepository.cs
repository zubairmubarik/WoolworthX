using MyDemoProject001.Application.Common.Interfaces;
using MyDemoProject001.Domain.Entities;
using System.Collections.Generic;

using System.Threading;
using System.Threading.Tasks;

using MongoDB.Driver;

namespace MyDemoProject001.Infrastructure.Repositories.ShopperHistory
{
    public class ShopperHistoryRepository : IShopperHistoryRepository
    {
        IMongoCollection<ShopperHistoryDocument> _shopperHistoryCollection;
        private readonly IMongoContext _mongoContext;
        public ShopperHistoryRepository(IDatabaseSettings settings, IMongoContext mongoContext)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _shopperHistoryCollection = database.GetCollection<ShopperHistoryDocument>("shopperHistory");

            // Assign the context
            _mongoContext = mongoContext;
        }
        public async Task<ShopperHistoryDocument> CreateAsync(ShopperHistoryDocument item, CancellationToken cancellationToken)
        {
            await _shopperHistoryCollection.InsertOneAsync(item, null, cancellationToken);
            return item;
        }

        public Task<long> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ShopperHistoryDocument>> GetAsync(CancellationToken cancellationToken)
        {
            var s = _shopperHistoryCollection.Find(shopperHistoryDocument => true).Project(x => x.Products).ToList();
            var s2 = _shopperHistoryCollection.Find(shopperHistoryDocument => true).Project(x =>x.Products.Find(x=>true)).ToList();

            return (await _shopperHistoryCollection.FindAsync(shopperHistoryDocument => true, null, cancellationToken)).ToList();
        }

       
        public async Task<List<ShopperHistoryDocument>> GetSortedAsync(FindOptions<ProductDocument> sortOptions, CancellationToken cancellationToken)
        {
           var s = _shopperHistoryCollection.Find(shopperHistoryDocument => true).Project(x => x.Products).ToList();
            //var a = _shopperHistoryCollection.Find(x=>x.Products),sortOptions,cancellationToken);
            //var filterDefinition = Builders<ShopperHistoryDocument>.Filter.Where(shopperHistoryDocument => true);
            //var class1 = (await _shopperHistoryCollection.FindAsync(filterDefinition=>true)).ToList();

            return (await _shopperHistoryCollection.FindAsync(filterDefinition => true, null, cancellationToken)).ToList();
        }

        public Task<ShopperHistoryDocument> GetAsync(string id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ShopperHistoryDocument> PatchAsync(string id, ShopperHistoryDocument item, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<ShopperHistoryDocument> UpdateAsync(string id, ShopperHistoryDocument item, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
