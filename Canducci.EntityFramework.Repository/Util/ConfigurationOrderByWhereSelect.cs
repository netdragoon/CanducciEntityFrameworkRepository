using System;
using System.Linq;
using Canducci.EntityFramework.Repository.Contracts.Interfaces;
namespace Canducci.EntityFramework.Repository.Util
{
    public class ConfigurationOrderByWhereSelect<T, TResult>: IConfiguration<T, TResult>
    {
        public IGroupOrderBy<T> OrderBy { get; set; }
        public IGroupWhere<T> Where { get; set; }
        public ISelect<T, TResult> Select { get; set; }

        public ConfigurationOrderByWhereSelect()
        {
            OrderBy = new GroupOrderBy<T>();
            Where = new GroupWhere<T>();
            Select = new Select<T, TResult>();
        }

        public IQueryable<TResult> Query(IQueryable<T> source)
        {
            if (Select.Item == null)
            {
                throw new Exception("Error: Select is null");
            }
            IQueryable<T> model = QueryableUtil.GetWhereQueryable(source, Where);            
            return QueryableUtil.GetOrderedQueryable(model, OrderBy)
                .Select(Select.Item);            
        }

        public static IConfiguration<T, TResult> Create(Action<ConfigurationOrderByWhereSelect<T, TResult>> configuration)
        {
            Action<ConfigurationOrderByWhereSelect<T, TResult>> conf = new Action<ConfigurationOrderByWhereSelect<T, TResult>>(configuration);
            ConfigurationOrderByWhereSelect<T, TResult> obj = new ConfigurationOrderByWhereSelect<T, TResult>();
            conf.Invoke(obj);
            return obj;
        }

    }
}
