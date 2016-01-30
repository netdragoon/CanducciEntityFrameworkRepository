using System.Linq;
using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
namespace Canducci.EntityFramework.Repository.Util
{
    public sealed class ConfigurationWhere<T>: IConfiguration<T>
    {
        public IGroupWhere<T> Where { get; set; }

        public ConfigurationWhere()
        {
            Where = new GroupWhere<T>();            
        }

        public IQueryable<T> Query(IQueryable<T> source)
        {
            return QueryableUtil.GetWhereQueryable(source, Where);
        }

        public static IConfiguration<T> Create(Action<ConfigurationWhere<T>> configuration)
        {
            Action<ConfigurationWhere<T>> conf = new Action<ConfigurationWhere<T>>(configuration);
            ConfigurationWhere<T> obj = new ConfigurationWhere<T>();
            conf.Invoke(obj);
            return obj;
        }
    }
}