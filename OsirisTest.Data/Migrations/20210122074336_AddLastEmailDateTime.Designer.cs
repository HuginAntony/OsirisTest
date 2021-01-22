﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OsirisTest.Data;

namespace OsirisTest.Data.Migrations
{
    [DbContext(typeof(OsirisContext))]
    [Migration("20210122074336_AddLastEmailDateTime")]
    partial class AddLastEmailDateTime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("OsirisTest.Data.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<decimal?>("FirstDepositAmount")
                        .HasColumnType("decimal(19,5)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("InsertedDateTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("LastEmailDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("LastUpdateDateTime")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("LastWagerAmount")
                        .HasColumnType("decimal(19,5)");

                    b.Property<DateTime?>("LastWagerDateTime")
                        .HasColumnType("datetime");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("OsirisTest.Data.Wager", b =>
                {
                    b.Property<Guid>("WagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(19,5)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertedDateTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("WagerDateTime")
                        .HasColumnType("datetime");

                    b.HasKey("WagerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Wager");
                });

            modelBuilder.Entity("OsirisTest.Data.Wager", b =>
                {
                    b.HasOne("OsirisTest.Data.Customer", "Customer")
                        .WithMany("Wagers")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Wager_CustomerId")
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("OsirisTest.Data.Customer", b =>
                {
                    b.Navigation("Wagers");
                });
#pragma warning restore 612, 618
        }
    }
}
