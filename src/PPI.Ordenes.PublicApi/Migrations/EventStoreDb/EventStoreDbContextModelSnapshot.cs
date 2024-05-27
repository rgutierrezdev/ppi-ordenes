﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PPI.Ordenes.Infrastructure.Data.Context;

#nullable disable

namespace PPI.Ordenes.PublicApi.Migrations.EventStoreDb
{
    [DbContext(typeof(EventStoreDbContext))]
    partial class EventStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Latin1_General_CI_AI")
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PPI.Ordenes.Core.Events.EventStore", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("VARCHAR(MAX)")
                        .HasComment("JSON serialized event");

                    b.Property<string>("MessageType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("OccurredOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.HasKey("Id");

                    b.ToTable("EventStores");
                });
#pragma warning restore 612, 618
        }
    }
}