#if NET45
using System.Threading;
using System.Threading.Tasks;
#endif
using System.Data.Entity;
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface IQueryCommand
    {
        int QueryCommand(string sql, params object[] parameters);
        int QueryCommand(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters);
#if NET45
        Task<int> QueryCommandAsync(string sql, params object[] parameters);
        Task<int> QueryCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters);
        Task<int> QueryCommandAsync(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters);
        Task<int> QueryCommandAsync(TransactionalBehavior transactionalBehavior, string sql, CancellationToken cancellationToken, params object[] parameters);
#endif
    }
}
