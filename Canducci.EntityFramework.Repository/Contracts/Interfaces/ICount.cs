using System;
using System.Linq.Expressions;
#if NET45
using System.Threading.Tasks;
#endif
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface ICount<T>
    {
        long Count();
        long Count(Expression<Func<T, bool>> where);
#if NET45
        Task<long> CountAsync();
        Task<long> CountAsync(Expression<Func<T, bool>> where);        
#endif
    }
}
