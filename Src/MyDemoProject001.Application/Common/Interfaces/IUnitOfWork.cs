using System;
using System.Threading.Tasks;

namespace MyDemoProject001.Application.Common.Interfaces
{
    public interface IUnitOfWork  : IDisposable
    {
        Task<bool> Commit();
        //ICustomerRepository CustomerRepository { get; }
        //Task<bool> SaveAsync();
    }
}
