using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface IGroupWhere<T>
    {
        IList<Expression<Func<T, bool>>> Items { get; }
        IGroupWhere<T> Add(Expression<Func<T, bool>> where);
    }
}
