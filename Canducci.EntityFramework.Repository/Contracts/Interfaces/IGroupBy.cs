using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
#if NET45
using System.Threading.Tasks;
#endif
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface IGroupBy<T>
    {        
        IList<TSelect> GroupBy<TKey, TSelect>(Expression<Func<T, TKey>> keySelector, Expression<Func<IGrouping<TKey, T>, TSelect>> select);                
        IList<TSelect> GroupBy<TKey, TSelect>(IGroupOrderBy<TSelect> groupOrderBy, IGroupWhere<TSelect> groupWhere, Expression<Func<T, TKey>> keySelector, Expression<Func<IGrouping<TKey, T>, TSelect>> select);
#if NET45
        Task<IList<TSelect>> GroupByAsync<TKey, TSelect>(Expression<Func<T, TKey>> keySelector, Expression<Func<IGrouping<TKey, T>, TSelect>> select);        
        Task<IList<TSelect>> GroupByAsync<TKey, TSelect>(IGroupOrderBy<TSelect> groupOrderBy, IGroupWhere<TSelect> groupWhere, Expression<Func<T, TKey>> keySelector, Expression<Func<IGrouping<TKey, T>, TSelect>> select);
#endif
    }
}
