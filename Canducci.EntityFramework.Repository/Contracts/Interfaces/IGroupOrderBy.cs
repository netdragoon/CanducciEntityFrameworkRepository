using Canducci.EntityFramework.Repository.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface IGroupOrderBy<T>
    {
        Dictionary<LambdaExpression, Order> Items();
        GroupOrderBy<T> Add<Tkey>(Expression<Func<T, Tkey>> orderBy, Order order = Order.Asc);
    }
}
