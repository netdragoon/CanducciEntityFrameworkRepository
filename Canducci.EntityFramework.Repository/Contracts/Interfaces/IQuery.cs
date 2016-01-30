using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface IQuery<T>
        where T: class, new()
    {        
        IQueryable<T> Query();
        IQueryable<T> Query(params Expression<Func<T, object>>[] includes);
        DbSqlQuery<T> Query(string sql, params object[] parameters);
        DbRawSqlQuery<T1> Query<T1>(string sql, params object[] parameters);
    }
}
