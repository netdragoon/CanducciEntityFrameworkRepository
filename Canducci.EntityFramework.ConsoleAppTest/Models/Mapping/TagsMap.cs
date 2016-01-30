using System.Data.Entity.ModelConfiguration;
namespace Canducci.EntityFramework.ConsoleAppTest.Models.Mapping
{
    public class TagsMap: EntityTypeConfiguration<Tags>
    {
        public TagsMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Description)
                .HasMaxLength(50);

            
            // Table & Column Mappings
            ToTable("Tags");
            Property(t => t.Id).HasColumnName("Id");
                     
            
                            
            Property(t => t.Description).HasColumnName("Description");

            HasMany(t => t.Notice)
                .WithRequired(r => r.Tags)
                .HasForeignKey(f => f.TagId)
                .WillCascadeOnDelete(false);

        }
    }
}
