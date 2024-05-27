﻿// <auto-generated />
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PPI.Ordenes.Infrastructure.Data.Context;

#nullable disable

namespace PPI.Ordenes.PublicApi.Migrations
{
    [DbContext(typeof(SqlDbContext))]
    [Migration("20240527060503_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Latin1_General_CI_AI")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PPI.Ordenes.Domain.Entities.AccountAggregate.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<decimal>("CuentaComitente")
                        .HasColumnType("DECIMAL(18,0)");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("DATETIME");

                    b.Property<int>("IDCuenta")
                        .HasColumnType("int");

                    b.Property<string>("PrimerNombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("SegundoNombre")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("TercerNombre")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("PPI.Ordenes.Domain.Entities.CustomerAggregate.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("DATE");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PPI.Ordenes.Domain.Entities.OrderAggregate.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("DATETIME");

                    b.Property<int>("IDCuenta")
                        .HasColumnType("int");

                    b.Property<int>("IDOrden")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDOrden"));

                    b.Property<decimal>("MontoTotal")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<string>("NombreActivo")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Operacion")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("DECIMAL(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PPI.Ordenes.Domain.Entities.CustomerAggregate.Customer", b =>
                {
                    b.OwnsOne("PPI.Ordenes.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(254)
                                .IsUnicode(false)
                                .HasColumnType("varchar(254)")
                                .HasColumnName("Email")
                                .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                                .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                            b1.HasKey("CustomerId");

                            b1.HasIndex("Address")
                                .IsUnique()
                                .HasFilter("[Email] IS NOT NULL");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Email");
                });

            modelBuilder.Entity("PPI.Ordenes.Domain.Entities.OrderAggregate.Order", b =>
                {
                    b.HasOne("PPI.Ordenes.Domain.Entities.AccountAggregate.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}