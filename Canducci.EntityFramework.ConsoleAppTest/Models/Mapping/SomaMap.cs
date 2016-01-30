using System.Data.Entity.ModelConfiguration;

namespace Canducci.EntityFramework.ConsoleAppTest.Models.Mapping
{
    public class SomaMap: EntityTypeConfiguration<Soma>
    {
        public SomaMap()
        {
            ToTable("Soma");

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(x => x.ItemId)
                .IsRequired();

            Property(x => x.Valor)
                .IsRequired();
        }
    }
}
