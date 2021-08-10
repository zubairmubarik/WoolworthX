using MongoDB.Driver;
using MyDemoProject001.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDemoProject001.Infrastructure.Persistence
{
    public class MongoContext : IMongoContext
    {
        public IClientSessionHandle Session { get; set; }
        public IDatabaseSettings _settings { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;


        public MongoContext(IDatabaseSettings settings)
        {
            // Here we store the initial application settings, including database connection string
            //  _configuration = configuration;
            // We need to keep all our commands inside a list to commit when we need
            _settings = settings;
            _commands = new List<Func<Task>>();
        }

        private void ConnectToMongo()
        {
            if (MongoClient != null)
            {
                return;
            }
            // You need to inject connection string and database name on appsettings
            MongoClient = new MongoClient(_settings.ConnectionString);
        }


        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommandAsync(Func<Task> func) =>  _commands.Add(func);

        public async Task<int> CommitChangesAsync()
        {
            ConnectToMongo();

            using (Session = await MongoClient.StartSessionAsync()) // Here we start our transaction
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync(); // And here we commit all our changes
            }

            return _commands.Count;
        }
    }
}
