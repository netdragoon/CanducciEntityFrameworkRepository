using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
using System.Linq;

namespace Canducci.EntityFramework.Repository.Util
{
    public class ConfigurationOrderBy<T> : IConfiguration<T>
    {
        public IGroupOrderBy<T> OrderBy { get; set; }

        public ConfigurationOrderBy()
        {
            OrderBy = new GroupOrderBy<T>();
        }

        public IQueryable<T> Query(IQueryable<T> source)
        {
            return QueryableUtil.GetOrderedQueryable(source, OrderBy).AsQueryable();
        }

        public static IConfiguration<T> Create(Action<ConfigurationOrderBy<T>> configuration)
        {
            Action<ConfigurationOrderBy<T>> conf = new Action<ConfigurationOrderBy<T>>(configuration);
            ConfigurationOrderBy<T> obj = new ConfigurationOrderBy<T>();
            conf.Invoke(obj);
            return obj;
        }
    }
}
