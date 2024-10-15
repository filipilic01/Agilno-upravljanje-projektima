﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rag_MicroService.Models;

#nullable disable

namespace Rag_MicroService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Rag_MicroService.Models.Entities.Rag", b =>
                {
                    b.Property<Guid>("RagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BacklogItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RagValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RagId");

                    b.HasIndex("BacklogItemId")
                        .IsUnique();

                    b.ToTable("Rags");

                    b.HasData(
                        new
                        {
                            RagId = new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f0"),
                            BacklogItemId = new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                            RagValue = "green"
                        },
                        new
                        {
                            RagId = new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f1"),
                            BacklogItemId = new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f4"),
                            RagValue = "red"
                        },
                        new
                        {
                            RagId = new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f2"),
                            BacklogItemId = new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f5"),
                            RagValue = "yellow"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}