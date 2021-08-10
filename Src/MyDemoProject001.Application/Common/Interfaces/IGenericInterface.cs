using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyDemoProject001.Application.Common.Interfaces
{
    public interface IGenericInterface<T>
    {
        Task<T> CreateAsync(T item, CancellationToken cancellationToken);       
        Task<T> UpdateAsync(string id, T item, CancellationToken cancellationToken);
        Task<T> PatchAsync(string id, T item, CancellationToken cancellationToken);
        Task<long> DeleteAsync(string id, CancellationToken cancellationToken);
        Task<List<T>> GetAsync(CancellationToken cancellationToken);
        Task<T> GetAsync(string id, CancellationToken cancellationToken);
    }
}
