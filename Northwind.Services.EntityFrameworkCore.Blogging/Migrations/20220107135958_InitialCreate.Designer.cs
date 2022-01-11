﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Northwind.Services.EntityFrameworkCore.Blogging.Context;

#nullable disable

namespace Northwind.Services.EntityFrameworkCore.Blogging.Migrations
{
    [DbContext(typeof(BloggingContext))]
    [Migration("20220107135958_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Northwind.Services.EntityFrameworkCore.Blogging.Entities.BlogArticle", b =>
                {
                    b.Property<int>("BlogArticleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BlogArticleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogArticleID"), 1L, 1);

                    b.Property<DateTime?>("DatePublished")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("PublisherId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Title")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("BlogArticleID");

                    b.HasIndex(new[] { "Title" }, "Title");

                    b.ToTable("BlogArticles");
                });
#pragma warning restore 612, 618
        }
    }
}
