using Canducci.EntityFramework.Repository.Util;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface IConfiguration<T>
    {
        IQueryable<T> Query(IQueryable<T> source);        
    }

    public interface IConfiguration<T, TResult>
    {
        IQueryable<TResult> Query(IQueryable<T> source);
    }

    public interface IConfiguration<T, T1, TResult>
    {
        TResult Query(IQueryable<T> source);
#if NET45
        Task<TResult> QueryAsync(IQueryable<T> source);
#endif

    }

    internal abstract class QueryableUtil
    {

        public static IQueryable<T> GetOrderedQueryable<T>(IQueryable<T> source, IGroupOrderBy<T> groupOrderBy)
        {
            if (groupOrderBy.Items().Count == 0) return source;
            IOrderedQueryable<T> modelOrdered = null;
            foreach (KeyValuePair<LambdaExpression, Order> item in groupOrderBy.Items())
            {

                modelOrdered = item.Value == Order.Asc ?
                        modelOrdered == null ?
                            Queryable.OrderBy(source, (dynamic)item.Key) :
                            Queryable.ThenBy(modelOrdered, (dynamic)item.Key) :
                        modelOrdered == null ?
                            Queryable.OrderByDescending(source, (dynamic)item.Key) :
                            Queryable.ThenByDescending(modelOrdered, (dynamic)item.Key);
            }
            return modelOrdered.AsQueryable();

        }

        public static IQueryable<T> GetWhereQueryable<T>(IQueryable<T> source, IGroupWhere<T> groupWhere)
        {
            foreach (Expression<Func<T, bool>> item in groupWhere.Items)
            {
                source = source.Where(item);
            }
            return source;
        }

        

    }
    internal abstract class ConfigurationUtil
    {
        public static IConfiguration<T> Call<T, TConfiguration>(Action<TConfiguration> configuration)
           where T : class, new()
           where TConfiguration : IConfiguration<T>

        {
            TConfiguration obj = Activator.CreateInstance<TConfiguration>();
            configuration.Invoke(obj);
            return obj;
        }

        public static IConfiguration<T, TResult> Call<T, TResult, TConfiguration>(Action<TConfiguration> configuration)
           where T : class, new()
           where TConfiguration : IConfiguration<T, TResult>

        {
            TConfiguration obj = Activator.CreateInstance<TConfiguration>();
            configuration.Invoke(obj);
            return obj;
        }

        public static IConfiguration<T, T, IPagedList<T>> PaginationCall<T, TConfiguration>(Action<TConfiguration> configuration)
           where T : class, new()
           where TConfiguration : IConfiguration<T, T, IPagedList<T>>

        {
            TConfiguration obj = Activator.CreateInstance<TConfiguration>();
            configuration.Invoke(obj);
            return obj;
        }

        public static IConfiguration<T, TResult, IPagedList<TResult>> PaginationCall<T, TResult, TConfiguration>(Action<TConfiguration> configuration)
           where T : class, new()
           where TConfiguration : IConfiguration<T, TResult, IPagedList<TResult>>

        {
            TConfiguration obj = Activator.CreateInstance<TConfiguration>();
            configuration.Invoke(obj);
            return obj;
        }
    }
}

