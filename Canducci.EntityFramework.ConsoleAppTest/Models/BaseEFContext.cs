using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Canducci.EntityFramework.ConsoleAppTest.Models.Mapping;

namespace Canducci.EntityFramework.ConsoleAppTest.Models
{
    public partial class BaseEFContext : DbContext
    {
        static BaseEFContext()
        {
            Database.SetInitializer<BaseEFContext>(null);            
        }

        public BaseEFContext()
            : base("Name=BaseEFContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Notice> Notices { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Soma> Soma { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NoticeMap());
            modelBuilder.Configurations.Add(new TagsMap());
            modelBuilder.Configurations.Add(new SomaMap());
        }
    }
}
