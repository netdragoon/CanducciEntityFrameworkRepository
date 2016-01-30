using Canducci.EntityFramework.Repository.Contracts;
namespace WebApplication1.Models
{
    public abstract class RepositoryNoticeContract: 
        Repository<Notice, BaseEFEntities>,
        IRepository<Notice, BaseEFEntities>
    {
        public RepositoryNoticeContract(BaseEFEntities ctx)
            : base(ctx)
        {
        }
    }
    public sealed class RepositoryNotice :
        RepositoryNoticeContract
    {
        public RepositoryNotice(BaseEFEntities ctx) 
            : base(ctx)
        {
        }
    }

    public abstract class RepositoryTagsContract :
        Repository<Tags, BaseEFEntities>,
        IRepository<Tags, BaseEFEntities>
    {
        public RepositoryTagsContract(BaseEFEntities ctx)
            : base(ctx)
        {
        }
    }
    public sealed class RepositoryTags :
        RepositoryTagsContract
    {
        public RepositoryTags(BaseEFEntities ctx)
            : base(ctx)
        {
        }
    }
}