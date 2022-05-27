﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Data;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(WebApplicationContext))]
    [Migration("20220527100326_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebApplication1.Chat", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("contactid")
                        .HasColumnType("longtext");

                    b.Property<string>("userid")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("WebApplication1.Contact", b =>
                {
                    b.Property<string>("contactid")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("username")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("last")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("lastdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.Property<string>("server")
                        .HasColumnType("longtext");

                    b.HasKey("contactid", "username");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("WebApplication1.Message", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<string>("content")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("sent")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("WebApplication1.User", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("nickname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("server")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
