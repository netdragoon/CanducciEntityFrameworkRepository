using System;
using System.Linq.Expressions;
#if NET45
using System.Threading.Tasks;
#endif
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface IFind<T> 
        where T : class, new()
    {
        T Find(params object[] key);
        T Find(Expression<Func<T, bool>> where);
#if NET45
        Task<T> FindAsync(params object[] key);
        Task<T> FindAsync(Expression<Func<T, bool>> where);
#endif
    }
}
