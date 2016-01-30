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
    public class ConfigurationOrderByWherePagination<T> : IConfiguration<T,T, IPagedList<T>>
    {
        public IGroupOrderBy<T> OrderBy { get; set; }
        public IGroupWhere<T> Where { get; set; }
        public int? Page { get; set; }
        public int Total { get; set; }

        public ConfigurationOrderByWherePagination()
        {
            OrderBy = new GroupOrderBy<T>();
            Where = new GroupWhere<T>();
            Page = 1;
            Total = 10;
        }       

        public IPagedList<T> Query(IQueryable<T> source)
        {
            if (Page.HasValue)
            {
                if (Page <= 0) Page = 1;
            }
            IQueryable<T> model = QueryableUtil.GetWhereQueryable(source, Where);
            return QueryableUtil.GetOrderedQueryable(model, OrderBy)
                .ToPagedList(Page ?? 1, Total);
        }

#if NET45
        public async Task<IPagedList<T>> QueryAsync(IQueryable<T> source)
        {
            if (Page.HasValue)
            {
                if (Page <= 0) Page = 1;
            }
            IQueryable<T> model = QueryableUtil.GetWhereQueryable(source, Where);
            IQueryable<T> modelOrdered = QueryableUtil.GetOrderedQueryable(model, OrderBy);
            int totalItemCount = await model.CountAsync();
            IList<T> listModel = await modelOrdered.Skip((Page.Value - 1) * Total).Take(Total).ToListAsync();
            return new StaticPagedList<T>(listModel, Page ?? 1, Total, totalItemCount);
        }
#endif
        public static IConfiguration<T, T, IPagedList<T>> Create(Action<ConfigurationOrderByWherePagination<T>> configuration)
        {
            Action<ConfigurationOrderByWherePagination<T>> conf = new Action<ConfigurationOrderByWherePagination<T>>(configuration);
            ConfigurationOrderByWherePagination<T> obj = new ConfigurationOrderByWherePagination<T>();
            conf.Invoke(obj);
            return obj;
        }

    }
}
