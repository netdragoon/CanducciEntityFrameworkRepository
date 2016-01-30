using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
using System.Linq;

namespace Canducci.EntityFramework.Repository.Util
{
    public class ConfigurationOrderBySelectLimit<T, TResult> : IConfiguration<T, TResult>
    {
        public IGroupOrderBy<T> OrderBy { get; set; }
        public ISelect<T, TResult> Select { get; set; }
        public int? Page { get; set; }
        public int Total { get; set; }

        public ConfigurationOrderBySelectLimit()
        {
            OrderBy = new GroupOrderBy<T>();
            Select = new Select<T, TResult>();
        }
        public IQueryable<TResult> Query(IQueryable<T> source)
        {
            if (Select.Item == null)
            {
                throw new Exception("Error: Select is null");
            }
            if (Page.HasValue)
            {
                if (Page <= 0) Page = 1;
            }
            return QueryableUtil.GetOrderedQueryable(source, OrderBy)
                .Skip((Page.Value - 1) * Total)
                .Take(Total)
                .Select(Select.Item)
                .AsQueryable();
        }

        public static IConfiguration<T, TResult> Create(Action<ConfigurationOrderBySelectLimit<T, TResult>> configuration)
        {
            Action<ConfigurationOrderBySelectLimit<T, TResult>> conf = new Action<ConfigurationOrderBySelectLimit<T, TResult>>(configuration);
            ConfigurationOrderBySelectLimit<T, TResult> obj = new ConfigurationOrderBySelectLimit<T, TResult>();
            conf.Invoke(obj);
            return obj;
        }

    }
}
