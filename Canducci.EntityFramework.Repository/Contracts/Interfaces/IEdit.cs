#if NET45
using System.Threading.Tasks;
#endif
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface IEdit<T> 
        where T : class, new()
    {
        bool Edit(T model);
#if NET45
        Task<bool> EditAsync(T model);
#endif
    }
}
