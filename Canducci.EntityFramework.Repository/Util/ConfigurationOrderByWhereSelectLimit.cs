using System;
using System.Linq;
using Canducci.EntityFramework.Repository.Contracts.Interfaces;
namespace Canducci.EntityFramework.Repository.Util
{
    public class ConfigurationOrderByWhereSelectLimit<T, TResult> : IConfiguration<T, TResult>
    {
        public IGroupOrderBy<T> OrderBy { get; set; }
        public IGroupWhere<T> Where { get; set; }
        public Select<T, TResult> Select { get; set; }
        public int? Page { get; set; }
        public int Total { get; set; }

        public ConfigurationOrderByWhereSelectLimit()
        {
            OrderBy = new GroupOrderBy<T>();
            Where = new GroupWhere<T>();
            Select = new Select<T, TResult>();
            Page = 1;
            Total = 10;
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
            IQueryable<T> model = QueryableUtil.GetWhereQueryable(source, Where);
            return QueryableUtil.GetOrderedQueryable(model, OrderBy)
                .Select(Select.Item)
                .Skip((Page.Value - 1) * Total)
                .Take(Total);
        }

        public static IConfiguration<T, TResult> Create(Action<ConfigurationOrderByWhereSelectLimit<T, TResult>> configuration)
        {
            Action<ConfigurationOrderByWhereSelectLimit<T, TResult>> conf = new Action<ConfigurationOrderByWhereSelectLimit<T, TResult>>(configuration);
            ConfigurationOrderByWhereSelectLimit<T, TResult> obj = new ConfigurationOrderByWhereSelectLimit<T, TResult>();
            conf.Invoke(obj);
            return obj;
        }
    }
}
