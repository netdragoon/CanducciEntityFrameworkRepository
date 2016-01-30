using PagedList;
using System;
using System.Linq.Expressions;
#if NET45
using System.Threading.Tasks;
#endif
using Canducci.EntityFramework.Repository.Util;
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface IPagination<T>
        where T: class, new()
    {

        IPagedList<T> Pagination<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int page, int total = 10);
        IPagedList<TResult> Pagination<TResult, TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, Expression<Func<T, TResult>> select, int page, int total = 10);
        IPagedList<T> Pagination<TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderBy, int page, int total = 10);
        IPagedList<TResult> Pagination<TResult, TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderBy, Expression<Func<T, TResult>> select, int page, int total = 10);

        IPagedList<T> Pagination(IGroupOrderBy<T> groupOrderBy, int page, int total = 10);        
        IPagedList<T> Pagination(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, int page, int total = 10);
        IPagedList<TResult> Pagination<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, TResult>> select, int page, int total = 10);
        IPagedList<TResult> Pagination<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select, int page, int total = 10);

        IPagedList<T> Pagination(IConfiguration<T, T, IPagedList<T>> configuration);
        IPagedList<TResult> Pagination<TResult>(IConfiguration<T, TResult, IPagedList<TResult>> configuration);

        IPagedList<T> Pagination<TConfiguration>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, T, IPagedList<T>>;
        IPagedList<TResult> Pagination<TConfiguration, TResult>(Action<TConfiguration> configuration)
            where TConfiguration: IConfiguration<T, TResult, IPagedList<TResult>>;
#if NET45

        Task<IPagedList<T>> PaginationAsync<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int page, int total = 10);
        Task<IPagedList<TResult>> PaginationAsync<TResult, TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, Expression<Func<T, TResult>> select, int page, int total = 10);
        Task<IPagedList<T>> PaginationAsync<TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderBy, int page, int total = 10);
        Task<IPagedList<TResult>> PaginationAsync<TResult, TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderBy, Expression<Func<T, TResult>> select, int page, int total = 10);

        Task<IPagedList<T>> PaginationAsync(IGroupOrderBy<T> groupOrderBy, int page, int total = 10);        
        Task<IPagedList<T>> PaginationAsync(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, int page, int total = 10);
        Task<IPagedList<TResult>> PaginationAsync<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, TResult>> select, int page, int total = 10);
        Task<IPagedList<TResult>> PaginationAsync<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select, int page, int total = 10);

        Task<IPagedList<T>> PaginationAsync(IConfiguration<T, T, IPagedList<T>> configuration);
        Task<IPagedList<TResult>> PaginationAsync<TResult>(IConfiguration<T, TResult, IPagedList<TResult>> configuration);

        Task<IPagedList<T>> PaginationAsync<TConfiguration>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, T, IPagedList<T>>;
        Task<IPagedList<TResult>> PaginationAsync<TConfiguration, TResult>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, TResult, IPagedList<TResult>>;

#endif
    }
}
