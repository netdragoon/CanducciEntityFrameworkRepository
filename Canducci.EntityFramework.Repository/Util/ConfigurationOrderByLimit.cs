using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
using System.Linq;
namespace Canducci.EntityFramework.Repository.Util
{
    public class ConfigurationOrderByLimit<T> : IConfiguration<T>
    {
        public IGroupOrderBy<T> OrderBy { get; set; }
        public int? Page { get; set; }
        public int Total { get; set; }

        public ConfigurationOrderByLimit()
        {
            OrderBy = new GroupOrderBy<T>();
        }

        public IQueryable<T> Query(IQueryable<T> source)
        {
            if (Page.HasValue)
            {
                if (Page <= 0) Page = 1;
            }
            return QueryableUtil.GetOrderedQueryable(source, OrderBy)
                .Skip((Page.Value - 1) * Total)
                .Take(Total)
                .AsQueryable();
        }

        public static IConfiguration<T> Create(Action<ConfigurationOrderByLimit<T>> configuration)
        {
            Action<ConfigurationOrderByLimit<T>> conf = new Action<ConfigurationOrderByLimit<T>>(configuration);
            ConfigurationOrderByLimit<T> obj = new ConfigurationOrderByLimit<T>();
            conf.Invoke(obj);
            return obj;
        }

    }
}
