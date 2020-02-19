﻿// <auto-generated />
using System;
using C_Sharp_WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace C_Sharp_WeddingPlanner.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200219000446_Migr8")]
    partial class Migr8
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("C_Sharp_WeddingPlanner.Models.Association", b =>
                {
                    b.Property<int>("AsId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserId");

                    b.Property<int>("WeddingId");

                    b.HasKey("AsId");

                    b.HasIndex("UserId");

                    b.HasIndex("WeddingId");

                    b.ToTable("Associations");
                });

            modelBuilder.Entity("C_Sharp_WeddingPlanner.Models.Login", b =>
                {
                    b.Property<string>("LoginEmail")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginPassword")
                        .IsRequired();

                    b.HasKey("LoginEmail");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("C_Sharp_WeddingPlanner.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("C_Sharp_WeddingPlanner.Models.Wedding", b =>
                {
                    b.Property<int>("WeddingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("Date");

                    b.Property<string>("NameOne")
                        .IsRequired();

                    b.Property<string>("NameTwo")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("WeddingId");

                    b.ToTable("Weddings");
                });

            modelBuilder.Entity("C_Sharp_WeddingPlanner.Models.Association", b =>
                {
                    b.HasOne("C_Sharp_WeddingPlanner.Models.User", "ToBeGuest")
                        .WithMany("WeddingToGo")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("C_Sharp_WeddingPlanner.Models.Wedding", "ToJoinWedding")
                        .WithMany("Guests")
                        .HasForeignKey("WeddingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
