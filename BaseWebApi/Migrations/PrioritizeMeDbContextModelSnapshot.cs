﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using prioritizemeServices.Database;

namespace prioritizemeServices.Migrations
{
    [DbContext(typeof(PrioritizeMeDbContext))]
    partial class PrioritizeMeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("prioritizemeServices.Core.Data.CurrentlySorting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CurrentlySortingItemId");

                    b.Property<Guid>("ListId");

                    b.Property<Guid>("LowerIndexId");

                    b.Property<Guid>("UpperIndexId");

                    b.HasKey("Id");

                    b.HasIndex("CurrentlySortingItemId");

                    b.HasIndex("ListId")
                        .IsUnique();

                    b.HasIndex("LowerIndexId")
                        .IsUnique();

                    b.HasIndex("UpperIndexId")
                        .IsUnique();

                    b.ToTable("CurrentlySortings");
                });

            modelBuilder.Entity("prioritizemeServices.Core.Data.PrioritizedList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CurrentlySortingId");

                    b.HasKey("Id");

                    b.HasIndex("CurrentlySortingId")
                        .IsUnique()
                        .HasFilter("[CurrentlySortingId] IS NOT NULL");

                    b.ToTable("PrioritizedLists");
                });

            modelBuilder.Entity("prioritizemeServices.Core.Data.PrioritizedListItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Completed");

                    b.Property<int?>("Index");

                    b.Property<Guid>("OwnerListId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("OwnerListId");

                    b.ToTable("PrioritizedListItems");
                });

            modelBuilder.Entity("prioritizemeServices.Core.Data.CurrentlySorting", b =>
                {
                    b.HasOne("prioritizemeServices.Core.Data.PrioritizedListItem", "CurrentlySortingItem")
                        .WithMany()
                        .HasForeignKey("CurrentlySortingItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("prioritizemeServices.Core.Data.PrioritizedList", "PrioritizedList")
                        .WithOne()
                        .HasForeignKey("prioritizemeServices.Core.Data.CurrentlySorting", "ListId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("prioritizemeServices.Core.Data.PrioritizedListItem", "LowerIndexItem")
                        .WithOne()
                        .HasForeignKey("prioritizemeServices.Core.Data.CurrentlySorting", "LowerIndexId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("prioritizemeServices.Core.Data.PrioritizedListItem", "UpperIndexItem")
                        .WithOne()
                        .HasForeignKey("prioritizemeServices.Core.Data.CurrentlySorting", "UpperIndexId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("prioritizemeServices.Core.Data.PrioritizedList", b =>
                {
                    b.HasOne("prioritizemeServices.Core.Data.CurrentlySorting", "CurrentlySorting")
                        .WithOne()
                        .HasForeignKey("prioritizemeServices.Core.Data.PrioritizedList", "CurrentlySortingId");
                });

            modelBuilder.Entity("prioritizemeServices.Core.Data.PrioritizedListItem", b =>
                {
                    b.HasOne("prioritizemeServices.Core.Data.PrioritizedList", "OwnerList")
                        .WithMany("Items")
                        .HasForeignKey("OwnerListId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}