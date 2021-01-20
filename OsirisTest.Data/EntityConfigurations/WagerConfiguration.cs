using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OsirisTest.Data.EntityConfigurations
{
    public class WagerConfiguration : IEntityTypeConfiguration<Wager>
    {
        public void Configure(EntityTypeBuilder<Wager> entity)
        {
            entity.ToTable("Wager");

            entity.Property(e => e.WagerId).ValueGeneratedNever();

            entity.Property(e => e.Amount).HasColumnType("decimal(19, 5)");

            entity.Property(e => e.InsertedDateTime).HasColumnType("datetime");

            entity.Property(e => e.WagerDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Customer)
                  .WithMany(p => p.Wagers)
                  .HasForeignKey(d => d.CustomerId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Wager_CustomerId");
        }
    }
}