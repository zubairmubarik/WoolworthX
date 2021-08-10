using System;
using System.Threading.Tasks;

namespace MyDemoProject001.Application.Common.Interfaces
{
    public interface IMongoContext : IDisposable
    {
        void AddCommandAsync(Func<Task> func);
        Task<int> CommitChangesAsync();
    }
}
