using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
using System.Linq;
namespace Canducci.EntityFramework.Repository.Util
{
    public class ConfigurationWhereLimit<T> : IConfiguration<T>
    {
        public IGroupWhere<T> Where { get; set; }
        public int? Page { get; set; }
        public int Total { get; set; }

        public ConfigurationWhereLimit()
        {
            Where = new GroupWhere<T>();
        }
        public IQueryable<T> Query(IQueryable<T> source)
        {
            if (Page.HasValue)
            {
                if (Page <= 0) Page = 1;
            }
            return QueryableUtil.GetWhereQueryable(source, Where)
                .Skip((Page.Value - 1) * Total)
                .Take(Total);
        }

        public static IConfiguration<T> Create(Action<ConfigurationWhereLimit<T>> configuration)
        {
            Action<ConfigurationWhereLimit<T>> conf = new Action<ConfigurationWhereLimit<T>>(configuration);
            ConfigurationWhereLimit<T> obj = new ConfigurationWhereLimit<T>();
            conf.Invoke(obj);
            return obj;
        }
    }
}
