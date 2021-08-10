using System.Threading.Tasks;

namespace MyDemoProject001.Application.Common.Interfaces
{
    public interface IExternalService<T>
    {
        Task<T> GetAsync();

        Task<T> GetAsync(string id);

        Task<T> PutAsync(string id, T item);

        Task<T> PostAsync(T item);

        Task<T> DeleteAsync(string id, T item);

    }
}
