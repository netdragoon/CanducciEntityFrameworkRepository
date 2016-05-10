using System.Collections.Generic;
#if NET45
using System.Threading.Tasks;
#endif
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface IAdd<T>
        where T : class, new()
    {
        T Add(T model);        
        IEnumerable<T> Add(IEnumerable<T> models);

#if NET45
        Task<T> AddAsync(T model);
        Task<IEnumerable<T>> AddAsync(IEnumerable<T> models);
#endif

    }
}
