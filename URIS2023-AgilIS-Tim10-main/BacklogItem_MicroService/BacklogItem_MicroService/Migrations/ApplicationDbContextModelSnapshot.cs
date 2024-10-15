﻿// <auto-generated />
using System;
using BacklogItem_MicroService.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BacklogItemMicroService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BacklogItem_MicroService.Models.Entities.AcceptanceCriteria", b =>
                {
                    b.Property<Guid>("AcceptanceCriteriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AcceptanceCriteriaText")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("BacklogItemId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AcceptanceCriteriaId");

                    b.HasIndex("BacklogItemId")
                        .IsUnique();

                    b.ToTable("AcceptanceCriterias");
                });

            modelBuilder.Entity("BacklogItem_MicroService.Models.Entities.BacklogItem", b =>
                {
                    b.Property<Guid>("BacklogItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BacklogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BacklogItemName")
                        .IsRequired()
                        .HasMaxLength(700)
                        .HasColumnType("nvarchar(700)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BacklogItemId");

                    b.ToTable("BacklogItems");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BacklogItem");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("BacklogItem_MicroService.Models.Entities.Description", b =>
                {
                    b.Property<Guid>("DescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BacklogItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DescriptionText")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("DescriptionId");

                    b.HasIndex("BacklogItemId")
                        .IsUnique();

                    b.ToTable("Descriptions");
                });

            modelBuilder.Entity("BacklogItem_MicroService.Models.Entities.StoryPoint", b =>
                {
                    b.Property<Guid>("StoryPointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BacklogItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StoryPointValue")
                        .HasColumnType("int");

                    b.HasKey("StoryPointId");

                    b.HasIndex("BacklogItemId")
                        .IsUnique();

                    b.ToTable("StoryPoints");
                });

            modelBuilder.Entity("BacklogItem_MicroService.Models.Entities.Epic", b =>
                {
                    b.HasBaseType("BacklogItem_MicroService.Models.Entities.BacklogItem");

                    b.HasDiscriminator().HasValue("Epic");
                });

            modelBuilder.Entity("BacklogItem_MicroService.Models.Entities.Functionality", b =>
                {
                    b.HasBaseType("BacklogItem_MicroService.Models.Entities.BacklogItem");

                    b.HasDiscriminator().HasValue("Functionality");
                });

            modelBuilder.Entity("BacklogItem_MicroService.Models.Entities.TaskEntity", b =>
                {
                    b.HasBaseType("BacklogItem_MicroService.Models.Entities.BacklogItem");

                    b.HasDiscriminator().HasValue("TaskEntity");
                });

            modelBuilder.Entity("BacklogItem_MicroService.Models.Entities.UserStory", b =>
                {
                    b.HasBaseType("BacklogItem_MicroService.Models.Entities.BacklogItem");

                    b.HasDiscriminator().HasValue("UserStory");
                });

            modelBuilder.Entity("BacklogItem_MicroService.Models.Entities.AcceptanceCriteria", b =>
                {
                    b.HasOne("BacklogItem_MicroService.Models.Entities.BacklogItem", "BacklogItem")
                        .WithOne("AcceptanceCriteria")
                        .HasForeignKey("BacklogItem_MicroService.Models.Entities.AcceptanceCriteria", "BacklogItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BacklogItem");
                });

            modelBuilder.Entity("BacklogItem_MicroService.Models.Entities.Description", b =>
                {
                    b.HasOne("BacklogItem_MicroService.Models.Entities.BacklogItem", "BacklogItem")
                        .WithOne("Description")
                        .HasForeignKey("BacklogItem_MicroService.Models.Entities.Description", "BacklogItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BacklogItem");
                });

            modelBuilder.Entity("BacklogItem_MicroService.Models.Entities.StoryPoint", b =>
                {
                    b.HasOne("BacklogItem_MicroService.Models.Entities.BacklogItem", "BacklogItem")
                        .WithOne("StoryPoint")
                        .HasForeignKey("BacklogItem_MicroService.Models.Entities.StoryPoint", "BacklogItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BacklogItem");
                });

            modelBuilder.Entity("BacklogItem_MicroService.Models.Entities.BacklogItem", b =>
                {
                    b.Navigation("AcceptanceCriteria")
                        .IsRequired();

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("StoryPoint")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}