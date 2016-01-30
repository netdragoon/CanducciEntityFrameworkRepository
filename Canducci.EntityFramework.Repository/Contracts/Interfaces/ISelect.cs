using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface ISelect<T, TResult>
    {
        Expression<Func<T, TResult>> Item { get;}
        void Set(Expression<Func<T, TResult>> select);
    }
}
