using System.Data.Entity;
namespace Canducci.EntityFramework.Repository.Contracts.Interfaces
{
    public interface ICreate<T>
    {
        DbSet<T1> Create<T1>()
            where T1 : class, new();
        T Create();
        T CreateAndAttach();
    }
}
