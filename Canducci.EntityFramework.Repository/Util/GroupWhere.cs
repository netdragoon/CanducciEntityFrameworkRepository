using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Canducci.EntityFramework.Repository.Util
{
    public class GroupWhere<T>: IGroupWhere<T>
    {
        public IList<Expression<Func<T, bool>>> Items { get; private set; }

        public GroupWhere()
        {
            Items = new List<Expression<Func<T, bool>>>();
        }
        public IGroupWhere<T> Add(Expression<Func<T, bool>> where)
        {
            Items.Add(where);
            return this;
        }

        public static IGroupWhere<T> Create()
        {
            return new GroupWhere<T>();
        }

        public static IGroupWhere<T> Create(Expression<Func<T, bool>> where)
        {
            return new GroupWhere<T>().Add(where);
        }
    }
}
