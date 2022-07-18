﻿// <auto-generated />
using DesafioStone.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesafioStone.Migrations
{
    [DbContext(typeof(ContaContext))]
    partial class ContaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("DesafioStone.Models.Conta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Saldo")
                        .HasColumnType("double");

                    b.Property<string>("Titular")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Contas");
                });
#pragma warning restore 612, 618
        }
    }
}