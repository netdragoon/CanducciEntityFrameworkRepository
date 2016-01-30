using Canducci.EntityFramework.ConsoleAppTest.Models;
using Canducci.EntityFramework.Repository.Contracts;
namespace Canducci.EntityFramework.ConsoleAppTest
{
    public class Tipo
    {
        public int TipoId { get; set; }
        public int Quantidade { get; set; }
    }
    public abstract class RepositoryNoticeContract: Repository<Notice, BaseEFContext>, IRepository<Notice, BaseEFContext>
    {
        public RepositoryNoticeContract(BaseEFContext Context)
            :base(Context)
        {
        }
    }
    public class RepositoryNotice : RepositoryNoticeContract
    {
        public RepositoryNotice(BaseEFContext Context)
            : base(Context)
        {
        }
    }
    public abstract class RepositoryTagsContract : Repository<Tags, BaseEFContext>, IRepository<Tags, BaseEFContext>
    {
        public RepositoryTagsContract(BaseEFContext Context)
            : base(Context)
        {
        }
    }
    public class RepositoryTags : RepositoryTagsContract
    {
        public RepositoryTags(BaseEFContext Context)
            : base(Context)
        {
        }
    }

    public abstract class RepositorySomaContract : Repository<Soma, BaseEFContext>, IRepository<Soma, BaseEFContext>
    {
        public RepositorySomaContract(BaseEFContext Context)
            : base(Context)
        {
        }
    }
    public class RepositorySoma : RepositorySomaContract
    {
        public RepositorySoma(BaseEFContext Context)
            : base(Context)
        {
        }
    }
}
