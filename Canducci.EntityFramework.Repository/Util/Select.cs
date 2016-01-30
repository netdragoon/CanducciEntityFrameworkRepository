using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
using System.Linq.Expressions;
namespace Canducci.EntityFramework.Repository.Util
{
    public sealed class Select<T, TResult>: ISelect<T, TResult>
    {
        public Expression<Func<T, TResult>> Item { get; private set; }

        public void Set(Expression<Func<T, TResult>> select)
        {
            Item = select;
        }
    }
}
