using Canducci.EntityFramework.Repository.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
#if NET45
using System.Threading.Tasks;
#endif
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface IAll<T>
    {
        IEnumerable<T> All(bool AsNoTracking = true);        
        IEnumerable<T> All(Expression<Func<T, bool>> where, bool AsNoTracking = true);
        IEnumerable<T> All<Tkey>(Expression<Func<T, Tkey>> orderBy, bool AsNoTracking = true);        
        IEnumerable<T> All<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, bool AsNoTracking = true);        
        IList<TResult> All<TResult, Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select);

        IEnumerable<T> All(IGroupOrderBy<T> groupOrderBy, bool AsNoTracking = true);
        IEnumerable<T> All(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, bool AsNoTracking = true);
        IList<TResult> All<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select);

        IEnumerable<T> All(IConfiguration<T> configuration);
        IList<TResult> All<TResult>(IConfiguration<T, TResult> configuration);

        IEnumerable<T> All<TConfiguration>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T>;

        IList<TResult> All<TConfiguration, TResult>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, TResult>;


#if NET45
        Task<IList<T>> AllAsync(bool AsNoTracking = true);
        Task<IList<T>> AllAsync(Expression<Func<T, bool>> where, bool AsNoTracking = true);
        Task<IList<T>> AllAsync<Tkey>(Expression<Func<T, Tkey>> orderBy, bool AsNoTracking = true);
        Task<IList<T>> AllAsync<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, bool AsNoTracking = true);
        Task<IList<TResult>> AllAsync<TResult, Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select);

        Task<IList<T>> AllAsync(IGroupOrderBy<T> groupOrderBy, bool AsNoTracking = true);
        Task<IList<T>> AllAsync(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, bool AsNoTracking = true);        
        Task<IList<TResult>> AllAsync<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select);

        Task<IList<T>> AllAsync(IConfiguration<T> configuration);
        Task<IList<TResult>> AllAsync<TResult>(IConfiguration<T, TResult> configuration);

        Task<IList<T>> AllAsync<TConfiguration>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T>;
        Task<IList<TResult>> AllAsync<TConfiguration, TResult>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, TResult>;


#endif
    }
}
