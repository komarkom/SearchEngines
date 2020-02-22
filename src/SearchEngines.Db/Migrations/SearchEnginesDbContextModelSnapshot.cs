﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SearchEngines.Db.Context;

namespace SearchEngines.Db.Migrations
{
    [DbContext(typeof(SearchEnginesDbContext))]
    partial class SearchEnginesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SearchEngines.Db.Entities.SearchRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("SearchText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SearchRequests");
                });

            modelBuilder.Entity("SearchEngines.Db.Entities.SearchResponse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasError")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long?>("SearchRequestId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SearchSystemId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SearchRequestId");

                    b.HasIndex("SearchSystemId");

                    b.ToTable("SearchResponses");
                });

            modelBuilder.Entity("SearchEngines.Db.Entities.SearchResult", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("HeaderLinkText")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PreviewData")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<long?>("SearchRequestId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SearchResponseId")
                        .HasColumnType("bigint");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("SearchRequestId");

                    b.HasIndex("SearchResponseId");

                    b.ToTable("SearchResults");
                });

            modelBuilder.Entity("SearchEngines.Db.Entities.SearchSystem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("SystemName")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("SearchSystems");
                });

            modelBuilder.Entity("SearchEngines.Db.Entities.SearchResponse", b =>
                {
                    b.HasOne("SearchEngines.Db.Entities.SearchRequest", "SearchRequest")
                        .WithMany()
                        .HasForeignKey("SearchRequestId");

                    b.HasOne("SearchEngines.Db.Entities.SearchSystem", "SearchSystem")
                        .WithMany()
                        .HasForeignKey("SearchSystemId");
                });

            modelBuilder.Entity("SearchEngines.Db.Entities.SearchResult", b =>
                {
                    b.HasOne("SearchEngines.Db.Entities.SearchRequest", null)
                        .WithMany("SearchResults")
                        .HasForeignKey("SearchRequestId");

                    b.HasOne("SearchEngines.Db.Entities.SearchResponse", "SearchResponse")
                        .WithMany("SearchResults")
                        .HasForeignKey("SearchResponseId");
                });
#pragma warning restore 612, 618
        }
    }
}
