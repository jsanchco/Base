﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SGI.DataEFCoreSQL;

namespace SGI.DataEFCoreSQL.Migrations
{
    [DbContext(typeof(EFContextSQL))]
    [Migration("20200428084710_Add_Field_Password")]
    partial class Add_Field_Password
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SGI.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Administrador"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Trabajador"
                        });
                });

            modelBuilder.Entity("SGI.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birthdate = new DateTime(1972, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Jesús",
                            RoleId = 1,
                            Surname = "Sánchez"
                        },
                        new
                        {
                            Id = 2,
                            Birthdate = new DateTime(1971, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Rubén",
                            RoleId = 1,
                            Surname = "Morales"
                        },
                        new
                        {
                            Id = 3,
                            Birthdate = new DateTime(1990, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Ariadna",
                            RoleId = 2,
                            Surname = "Pérez"
                        },
                        new
                        {
                            Id = 4,
                            Birthdate = new DateTime(2008, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Daniela",
                            RoleId = 2,
                            Surname = "Aceituno"
                        },
                        new
                        {
                            Id = 5,
                            Birthdate = new DateTime(2000, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Pedro",
                            RoleId = 1,
                            Surname = "González"
                        });
                });

            modelBuilder.Entity("SGI.Domain.Entities.User", b =>
                {
                    b.HasOne("SGI.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
