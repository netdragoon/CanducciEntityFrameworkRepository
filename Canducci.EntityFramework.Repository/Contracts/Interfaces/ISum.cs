using System;
using System.Linq.Expressions;
#if NET45
using System.Threading.Tasks;
#endif
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface ISum<T>
        where T : class, new()
    {
        decimal Sum(Expression<Func<T, decimal>> selector);
        decimal? Sum(Expression<Func<T, decimal?>> selector);
        double Sum(Expression<Func<T, double>> selector);
        double? Sum(Expression<Func<T, double?>> selector);
        float Sum(Expression<Func<T, float>> selector);
        float? Sum(Expression<Func<T, float?>> selector);
        int Sum(Expression<Func<T, int>> selector);
        int? Sum(Expression<Func<T, int?>> selector);
        long Sum(Expression<Func<T, long>> selector);
        long? Sum(Expression<Func<T, long?>> selector);

        decimal Sum(Expression<Func<T, bool>> where, Expression<Func<T, decimal>> selector);
        decimal? Sum(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> selector);
        double Sum(Expression<Func<T, bool>> where, Expression<Func<T, double>> selector);
        double? Sum(Expression<Func<T, bool>> where, Expression<Func<T, double?>> selector);
        float Sum(Expression<Func<T, bool>> where, Expression<Func<T, float>> selector);
        float? Sum(Expression<Func<T, bool>> where, Expression<Func<T, float?>> selector);
        int Sum(Expression<Func<T, bool>> where, Expression<Func<T, int>> selector);
        int? Sum(Expression<Func<T, bool>> where, Expression<Func<T, int?>> selector);
        long Sum(Expression<Func<T, bool>> where, Expression<Func<T, long>> selector);
        long? Sum(Expression<Func<T, bool>> where, Expression<Func<T, long?>> selector);

#if NET45
        Task<decimal> SumAsync(Expression<Func<T, decimal>> selector);
        Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector);
        Task<double> SumAsync(Expression<Func<T, double>> selector);
        Task<double?> SumAsync(Expression<Func<T, double?>> selector);
        Task<float> SumAsync(Expression<Func<T, float>> selector);
        Task<float?> SumAsync(Expression<Func<T, float?>> selector);
        Task<int> SumAsync(Expression<Func<T, int>> selector);
        Task<int?> SumAsync(Expression<Func<T, int?>> selector);
        Task<long> SumAsync(Expression<Func<T, long>> selector);
        Task<long?> SumAsync(Expression<Func<T, long?>> selector);

        Task<decimal> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, decimal>> selector);
        Task<decimal?> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> selector);
        Task<double> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, double>> selector);
        Task<double?> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, double?>> selector);
        Task<float> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, float>> selector);
        Task<float?> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, float?>> selector);
        Task<int> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, int>> selector);
        Task<int?> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, int?>> selector);
        Task<long> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, long>> selector);
        Task<long?> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, long?>> selector);
#endif
    }
}
