using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PagedList;
using Canducci.EntityFramework.Repository.Contracts.Interfaces;
#if NET45
using System.Threading.Tasks;
#endif
namespace Canducci.EntityFramework.Repository.Util
{
    public sealed class ConfigurationOrderByWherePaginationSelect<T, TResult> : IConfiguration<T, TResult, IPagedList<TResult>>
    {
        public IGroupOrderBy<T> OrderBy { get; set; }
        public IGroupWhere<T> Where { get; set; }
        public ISelect<T, TResult> Select { get; set; }
        public int? Page { get; set; }
        public int Total { get; set; }

        public ConfigurationOrderByWherePaginationSelect()
        {
            OrderBy = new GroupOrderBy<T>();
            Where = new GroupWhere<T>();
            Select = new Select<T, TResult>();
            Page = 1;
            Total = 10;
        }

        public IPagedList<TResult> Query(IQueryable<T> source)
        {
            if (Select.Item == null)
            {
                throw new Exception("Error: Select is null");
            }
            if (Page.HasValue)
            {
                if (Page <= 0) Page = 1;
            }
            IQueryable<T> model = QueryableUtil.GetWhereQueryable(source, Where);
            return QueryableUtil.GetOrderedQueryable(model, OrderBy)
                .Select(Select.Item)
                .ToPagedList(Page ?? 1, Total);
        }
#if NET45
        public async Task<IPagedList<TResult>> QueryAsync(IQueryable<T> source)
        {            
            if (Page.HasValue)
            {
                if (Page <= 0) Page = 1;
            }
            IQueryable<T> model = QueryableUtil.GetWhereQueryable(source, Where);
            IQueryable<T> modelOrdered = QueryableUtil.GetOrderedQueryable(model, OrderBy);            
            int totalItemCount = await model.CountAsync();
            IList<TResult> listModel = await modelOrdered.Select(Select.Item).Skip((Page.Value - 1) * Total).Take(Total).ToListAsync();
            return new StaticPagedList<TResult>(listModel, Page ?? 1, Total, totalItemCount);

        }
#endif
        public static IConfiguration<T, TResult, IPagedList<TResult>> Create(Action<ConfigurationOrderByWherePaginationSelect<T, TResult>> configuration)
        {
            Action<ConfigurationOrderByWherePaginationSelect<T, TResult>> conf = new Action<ConfigurationOrderByWherePaginationSelect<T, TResult>>(configuration);
            ConfigurationOrderByWherePaginationSelect<T, TResult> obj = new ConfigurationOrderByWherePaginationSelect<T, TResult>();
            conf.Invoke(obj);
            return obj;
        }
    }
}
