using System;
using System.Collections.Generic;
using System.Linq.Expressions;
#if NET45
using System.Threading.Tasks;
#endif
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface IDelete<T> where T : class, new()
    {
        bool Delete(T model);
        bool Delete(IEnumerable<T> model);
        bool Delete(params object[] key);
        bool Delete(Expression<Func<T, bool>> where);
#if NET45
        Task<bool> DeleteAsync(IEnumerable<T> model);
        Task<bool> DeleteAsync(T model);
        Task<bool> DeleteAsync(params object[] key);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> where);
#endif
    }
}
