using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
#if NET45
using System.Threading.Tasks;
#endif
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{

    public interface ILists<T>
    {
        IList<T> List(bool AsNoTracking = true);        
        IList<T> List<Tkey>(Expression<Func<T, Tkey>> orderBy, bool AsNoTracking = true);
        IList<T> List(Expression<Func<T, bool>> where, bool AsNoTracking = true);
        IList<T> List<Tkey>(Expression<Func<T, Tkey>> orderBy, int page, int total = 10, bool AsNoTracking = true);        
        IList<T> List<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, bool AsNoTracking);
        IList<T> List<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, int page, int total = 10, bool AsNoTracking = true);        
        IList<TResult> List<TResult, Tkey>(Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select);        
        IList<TResult> List<TResult, Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select);
        IList<TResult> List<TResult, Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select, int page, int total = 10);

        IList<T> List(IGroupOrderBy<T> groupOrderBy, bool AsNoTracking = true);        
        IList<T> List(IGroupOrderBy<T> groupOrderBy, int page, int total = 10, bool AsNoTracking = true);
        IList<T> List(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, bool AsNoTracking);
        IList<T> List(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, int page, int total = 10, bool AsNoTracking = true);

        IList<TResult> List<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, TResult>> select);
        IList<TResult> List<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select);
        IList<TResult> List<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select, int page, int total = 10);

        IList<T> List(IConfiguration<T> configuration);
        IList<TResult> List<TResult>(IConfiguration<T, TResult> configuration);

        IList<T> List<TConfiguration>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T>;
        IList<TResult> List<TConfiguration, TResult>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, TResult>;

#if NET45
        Task<IList<T>> ListAsync(bool AsNoTracking = true);               
        Task<IList<T>> ListAsync<Tkey>(Expression<Func<T, Tkey>> orderBy, bool AsNoTracking = true);
        Task<IList<T>> ListAsync(Expression<Func<T, bool>> where, bool AsNoTracking = true);        
        Task<IList<T>> ListAsync<Tkey>(Expression<Func<T, Tkey>> orderBy, int page, int total = 10, bool AsNoTracking = true);                
        Task<IList<T>> ListAsync<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, bool AsNoTracking);
        Task<IList<T>> ListAsync<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, int page, int total = 10, bool AsNoTracking = true);
        Task<IList<TResult>> ListAsync<TResult, Tkey>(Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select);        
        Task<IList<TResult>> ListAsync<TResult, Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select);
        Task<IList<TResult>> ListAsync<TResult, Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select, int page, int total = 10);

        Task<IList<T>> ListAsync(IGroupOrderBy<T> groupOrderBy, bool AsNoTracking = true);
        Task<IList<T>> ListAsync(IGroupOrderBy<T> groupOrderBy, int page, int total = 10, bool AsNoTracking = true);
        Task<IList<T>> ListAsync(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, bool AsNoTracking);
        Task<IList<T>> ListAsync(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, int page, int total = 10, bool AsNoTracking = true);

        Task<IList<TResult>> ListAsync<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, TResult>> select);
        Task<IList<TResult>> ListAsync<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select);
        Task<IList<TResult>> ListAsync<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select, int page, int total = 10);

        Task<IList<T>> ListAsync(IConfiguration<T> configuration);
        Task<IList<TResult>> ListAsync<TResult>(IConfiguration<T, TResult> configuration);

        Task<IList<T>> ListAsync<TConfiguration>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T>;
        Task<IList<TResult>> ListAsync<TConfiguration, TResult>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, TResult>;
#endif
    }
}

