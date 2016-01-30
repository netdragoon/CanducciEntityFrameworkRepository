using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
using System.Linq;

namespace Canducci.EntityFramework.Repository.Util
{
    public class ConfigurationWhereSelectLimit<T, TResult> : IConfiguration<T, TResult>
    {
        public IGroupWhere<T> Where { get; set; }
        public ISelect<T, TResult> Select { get; set; }
        public int? Page { get; set; }
        public int Total { get; set; }

        public ConfigurationWhereSelectLimit()
        {
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
            return QueryableUtil.GetWhereQueryable(source, Where)
                .Select(Select.Item)
                .Skip((Page.Value - 1) * Total)
                .Take(Total);
        }

        public static IConfiguration<T, TResult> Create(Action<ConfigurationWhereSelectLimit<T, TResult>> configuration)
        {
            Action<ConfigurationWhereSelectLimit<T, TResult>> conf = new Action<ConfigurationWhereSelectLimit<T, TResult>>(configuration);
            ConfigurationWhereSelectLimit<T, TResult> obj = new ConfigurationWhereSelectLimit<T, TResult>();
            conf.Invoke(obj);
            return obj;
        }

    }
}
