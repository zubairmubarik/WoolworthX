using Microsoft.Extensions.DependencyInjection;
using MyDemoProject001.Application.Common.Helpers;
using MyDemoProject001.Application.Common.Interfaces;
using MyDemoProject001.Application.Common.Models;
using MyDemoProject001.Infrastructure.Persistence;
using MyDemoProject001.Infrastructure.Repositories.Product;
using MyDemoProject001.Infrastructure.Repositories.ShopperHistory;
using MyDemoProject001.Infrastructure.Services;

namespace MyDemoProject001.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        { 
            
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IShopperHistoryRepository, ShopperHistoryRepository>();

            services.AddTransient<IMongoContext, MongoContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IExternalService<CustomerDto>, ExternalCustomerService>();

            services.AddTransient<ITrolleyCalculator, TrolleyCalculator>();

            services.AddHttpClient(); /* Add IHTTPClientFactory and related services*/           

            return services;
        }
    }
}
