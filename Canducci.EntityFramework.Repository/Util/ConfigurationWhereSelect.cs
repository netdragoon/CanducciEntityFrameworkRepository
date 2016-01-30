using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
using System.Linq;

namespace Canducci.EntityFramework.Repository.Util
{
    public class ConfigurationWhereSelect<T, TResult> : IConfiguration<T, TResult>
    {
        public IGroupWhere<T> Where { get; set; }
        public ISelect<T, TResult> Select { get; set; }

        public ConfigurationWhereSelect()
        {
            Where = new GroupWhere<T>();
            Select = new Select<T, TResult>();
        } 

        public IQueryable<TResult> Query(IQueryable<T> source)
        {
            if (Select.Item == null)
            {
                throw new Exception("Error: Select is null");
            }            
            return QueryableUtil.GetWhereQueryable(source, Where)
                .Select(Select.Item);

        }

        public static IConfiguration<T, TResult> Create(Action<ConfigurationWhereSelect<T, TResult>> configuration)
        {
            Action<ConfigurationWhereSelect<T, TResult>> conf = new Action<ConfigurationWhereSelect<T, TResult>>(configuration);
            ConfigurationWhereSelect<T, TResult> obj = new ConfigurationWhereSelect<T, TResult>();
            conf.Invoke(obj);
            return obj;
        }
    }
}
