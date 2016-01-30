#if NET45
using System.Threading.Tasks;
#endif
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface ISave
    {
        int Save();
#if NET45
        Task<int> SaveAsync();
#endif
    }
}
