﻿// <auto-generated />
using System;
using Desertnik.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Desertnik.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250112151346_Migration5")]
    partial class Migration5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Desertnik.Data.Ingredient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Desertnik.Data.Recipe", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Desertnik.Data.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "8867b3f8-a8ff-40b1-bafd-f103d66b2fe8",
                            Password = "AQAAAAIAAYagAAAAEAoSuZtkCSMGW44yedOSdXMZk3Of4Vvi3xqmkva8y0gN80+FkYAfU3iyGthq/H0MkQ==",
                            Role = "Admin",
                            Username = "admin"
                        },
                        new
                        {
                            Id = "d762dfdd-47bd-4942-b389-12495a89d6b0",
                            Password = "AQAAAAIAAYagAAAAELkPVqV55kE+exaZZmEVr/O1ruB9Cz/IV1vX3UhPbMAmL/DhD+hpSzBCW+FMReFaSg==",
                            Role = "User",
                            Username = "test"
                        });
                });

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.Property<string>("IngredientsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RecipesId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IngredientsId", "RecipesId");

                    b.HasIndex("RecipesId");

                    b.ToTable("IngredientRecipe");
                });

            modelBuilder.Entity("Desertnik.Data.Recipe", b =>
                {
                    b.HasOne("Desertnik.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.HasOne("Desertnik.Data.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Desertnik.Data.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
