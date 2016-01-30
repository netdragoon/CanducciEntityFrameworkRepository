using System.Data.Entity.ModelConfiguration;

namespace Canducci.EntityFramework.ConsoleAppTest.Models.Mapping
{
    public class NoticeMap : EntityTypeConfiguration<Notice>
    {
        public NoticeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Title)
                .HasMaxLength(50);

            Property(t => t.Texto)
                .HasColumnType("Text");

            // Table & Column Mappings
            ToTable("Notice");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Title).HasColumnName("Title");
            Property(t => t.Texto).HasColumnName("Texto");
            Property(t => t.Date).HasColumnName("Date");
            Property(t => t.Active).HasColumnName("Active");

            HasRequired(t => t.Tags)
                .WithMany(m => m.Notice)
                .HasForeignKey(f => f.TagId)
                .WillCascadeOnDelete(false);
        }
    }
}
