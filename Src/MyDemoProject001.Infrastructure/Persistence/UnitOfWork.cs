using MyDemoProject001.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace MyDemoProject001.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;

        public UnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            var changeAmount = await _context.CommitChangesAsync();

            return changeAmount > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        //private IDatabaseSettings _settings;
        ////public ICustomerRepository CustomerRepository => new CustomerRepository(_settings);
        //public UnitOfWork(IDatabaseSettings settings,ICustomerRepository customerRepository)
        //{
        //    _settings = settings;

        //    var client = new MongoClient(settings.ConnectionString);
        //    var database = client.GetDatabase(settings.DatabaseName);

        //   // _customersCollection = database.GetCollection<CustomerDocument>("customer");
        //}

        //public Task<bool> SaveAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
