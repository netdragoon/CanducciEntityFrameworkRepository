using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canducci.EntityFramework.Repository.Util
{
    public class ConfigurationOrderByPaginationSelect<T, TResult> : IConfiguration<T, TResult, IPagedList<TResult>>
    {
        public IGroupOrderBy<T> OrderBy { get; set; }
        public ISelect<T, TResult> Select { get; set; }
        public int? Page { get; set; }
        public int Total { get; set; }
        public ConfigurationOrderByPaginationSelect()
        {
            OrderBy = new GroupOrderBy<T>();
            Select = new Select<T, TResult>();
            Page = 1;
            Total = 10;
        }

        public IPagedList<TResult> Query(IQueryable<T> source)
        {
            if (Page.HasValue)
            {
                if (Page <= 0) Page = 1;
            }
            return QueryableUtil.GetOrderedQueryable(source, OrderBy)
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
            IQueryable<T> modelOrdered = QueryableUtil.GetOrderedQueryable(source, OrderBy);
            int totalItemCount = await source.CountAsync();
            IList<TResult> listModel = await modelOrdered.Select(Select.Item).Skip((Page.Value - 1) * Total).Take(Total).ToListAsync();
            return new StaticPagedList<TResult>(listModel, Page ?? 1, Total, totalItemCount);
        }
#endif
    }
}
