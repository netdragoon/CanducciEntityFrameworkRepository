using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Canducci.EntityFramework.Repository.Util
{
    public sealed class GroupOrderBy<T>: IGroupOrderBy<T>
    {
        private Dictionary<LambdaExpression, Order> items { get; set; }

        public GroupOrderBy()
        {
            items = new Dictionary<LambdaExpression, Order>();
        }

        public GroupOrderBy<T> Add<Tkey>(Expression<Func<T, Tkey>> orderBy, Order order = Order.Asc)
        {
            items.Add(orderBy, order);
            return this;
        }

        public Dictionary<LambdaExpression, Order> Items()
        {
            return items;
        }

        public static IGroupOrderBy<T> Create()
        {
            return new GroupOrderBy<T>();
        }

        public static IGroupOrderBy<T> Create<Tkey>(Expression<Func<T, Tkey>> orderBy, Order order = Order.Asc)
        {
            return new GroupOrderBy<T>().Add(orderBy, order);
        }
    }
}
