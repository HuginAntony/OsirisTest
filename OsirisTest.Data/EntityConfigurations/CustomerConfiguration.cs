using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OsirisTest.Data.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();

            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

            entity.Property(e => e.EmailAddress)
                  .IsRequired()
                  .HasMaxLength(255)
                  .IsUnicode(false);

            entity.Property(e => e.FirstDepositAmount).HasColumnType("decimal(19, 5)");

            entity.Property(e => e.FirstName)
                  .IsRequired()
                  .HasMaxLength(255)
                  .IsUnicode(false);

            entity.Property(e => e.InserteDateTime).HasColumnType("datetime");

            entity.Property(e => e.LastName)
                  .IsRequired()
                  .HasMaxLength(255)
                  .IsUnicode(false);

            entity.Property(e => e.LastUpdateDateTime).HasColumnType("datetime");

            entity.Property(e => e.LastWagerAmount).HasColumnType("decimal(19, 5)");

            entity.Property(e => e.LastWagerDateTime).HasColumnType("datetime");
        }
    }
}
