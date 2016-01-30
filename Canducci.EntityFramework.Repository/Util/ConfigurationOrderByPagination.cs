using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using PagedList;
using System.Collections.Generic;
using System;

namespace Canducci.EntityFramework.Repository.Util
{
    public class ConfigurationOrderByPagination<T> : IConfiguration<T, T, IPagedList<T>>
    {
        public IGroupOrderBy<T> OrderBy { get; set; }        
        public int? Page { get; set; }
        public int Total { get; set; }
        public ConfigurationOrderByPagination()
        {
            OrderBy = new GroupOrderBy<T>();            
            Page = 1;
            Total = 10;
        }
        public IPagedList<T> Query(IQueryable<T> source)
        {
            if (Page.HasValue)
            {
                if (Page <= 0) Page = 1;
            }            
            return QueryableUtil.GetOrderedQueryable(source, OrderBy)
                .ToPagedList(Page ?? 1, Total);
        }
#if NET45
        public async Task<IPagedList<T>> QueryAsync(IQueryable<T> source)
        {
            if (Page.HasValue)
            {
                if (Page <= 0) Page = 1;
            }            
            IQueryable<T> modelOrdered = QueryableUtil.GetOrderedQueryable(source, OrderBy);
            int totalItemCount = await source.CountAsync();
            IList<T> listModel = await modelOrdered.Skip((Page.Value - 1) * Total).Take(Total).ToListAsync();
            return new StaticPagedList<T>(listModel, Page ?? 1, Total, totalItemCount);
        }
#endif
        public static IConfiguration<T, T, IPagedList<T>> Create(Action<ConfigurationOrderByPagination<T>> configuration)
        {
            Action<ConfigurationOrderByPagination<T>> conf = new Action<ConfigurationOrderByPagination<T>>(configuration);
            ConfigurationOrderByPagination<T> obj = new ConfigurationOrderByPagination<T>();
            conf.Invoke(obj);
            return obj;
        }
    }
}
