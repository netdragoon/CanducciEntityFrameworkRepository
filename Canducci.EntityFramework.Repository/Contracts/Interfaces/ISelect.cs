using System;
using System.Linq.Expressions;
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface ISelect<T, TResult>
    {
        Expression<Func<T, TResult>> Item { get; }
        void Set(Expression<Func<T, TResult>> select);
    }
}
