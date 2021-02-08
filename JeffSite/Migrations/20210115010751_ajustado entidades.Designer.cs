﻿// <auto-generated />
using System;
using JeffSite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JeffSite.Migrations
{
    [DbContext(typeof(JeffContext))]
    [Migration("20210115010751_ajustado entidades")]
    partial class ajustadoentidades
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("JeffSite.Models.Carousel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Link")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PathImage")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Carousels");
                });

            modelBuilder.Entity("JeffSite.Models.Configuracao", b =>
                {
                    b.Property<int>("Cod")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ImgLogo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ImgProfile")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UrlMercadoLivre")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Cod");

                    b.ToTable("Configuracao");
                });

            modelBuilder.Entity("JeffSite.Models.Leitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NameImg")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nickname")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PathImg")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Leitors");
                });

            modelBuilder.Entity("JeffSite.Models.Livro.Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ImgName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("TagsId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("WhereToBuyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TagsId");

                    b.HasIndex("WhereToBuyId");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("JeffSite.Models.Livro.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("JeffSite.Models.Livro.WhereToBuy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("IconFA")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("WhereToBuys");
                });

            modelBuilder.Entity("JeffSite.Models.SocialMidia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("IconFA")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Url")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("SocialMidia");
                });

            modelBuilder.Entity("JeffSite.Models.User", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Pass")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserName");

                    b.ToTable("User");
                });

            modelBuilder.Entity("JeffSite.Models.Livro.Livro", b =>
                {
                    b.HasOne("JeffSite.Models.Livro.Tag", "Tags")
                        .WithMany("Livros")
                        .HasForeignKey("TagsId");

                    b.HasOne("JeffSite.Models.Livro.WhereToBuy", "WhereToBuy")
                        .WithMany("Livros")
                        .HasForeignKey("WhereToBuyId");
                });
#pragma warning restore 612, 618
        }
    }
}
