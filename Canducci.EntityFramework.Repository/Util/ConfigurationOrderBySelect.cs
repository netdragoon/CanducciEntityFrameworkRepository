using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
using System.Linq;

namespace Canducci.EntityFramework.Repository.Util
{
    public class ConfigurationOrderBySelect<T, TResult> : IConfiguration<T, TResult>
    {
        public IGroupOrderBy<T> OrderBy { get; set; }        
        public ISelect<T, TResult> Select { get; set; }

        public ConfigurationOrderBySelect()
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
            return QueryableUtil.GetOrderedQueryable(source, OrderBy)
                .Select(Select.Item);
        }
        public static IConfiguration<T, TResult> Create(Action<ConfigurationOrderBySelect<T, TResult>> configuration)
        {
            Action<ConfigurationOrderBySelect<T, TResult>> conf = new Action<ConfigurationOrderBySelect<T, TResult>>(configuration);
            ConfigurationOrderBySelect<T, TResult> obj = new ConfigurationOrderBySelect<T, TResult>();
            conf.Invoke(obj);
            return obj;
        }
    }
}
