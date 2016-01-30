using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
using System.Linq;
namespace Canducci.EntityFramework.Repository.Util
{
    public class ConfigurationOrderByWhere<T> : IConfiguration<T>
    {
        public IGroupOrderBy<T> OrderBy { get; set; }
        public IGroupWhere<T> Where { get; set; }

        public ConfigurationOrderByWhere()
        {
            OrderBy = new GroupOrderBy<T>();
            Where = new GroupWhere<T>();            
        }

        public IQueryable<T> Query(IQueryable<T> source)
        {
            IQueryable<T> model = QueryableUtil.GetWhereQueryable(source, Where);
            return QueryableUtil.GetOrderedQueryable(model, OrderBy);            
        }

        public static IConfiguration<T> Create(Action<ConfigurationOrderByWhere<T>> configuration)
        {
            Action<ConfigurationOrderByWhere<T>> conf = new Action<ConfigurationOrderByWhere<T>>(configuration);
            ConfigurationOrderByWhere<T> obj = new ConfigurationOrderByWhere<T>();
            conf.Invoke(obj);
            return obj;
        }
    }
}
