﻿// <auto-generated />
using System;
using CoreAPI.DataBase.SQLServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoreAPI.Migrations
{
    [DbContext(typeof(Context))]
    partial class RegistroDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoreAPI.Data.Entity.RegistroPessoa", b =>
                {
                    b.Property<string>("CPF")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("DataNascimento")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Genero")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Status")
                        .HasColumnType("nchar(10)");

                    b.HasKey("CPF");

                    b.HasIndex("Nome")
                        .HasDatabaseName("IX_Pessoa_Nome");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("Nome"), false);

                    b.ToTable("RegistroPessoa");
                });

            modelBuilder.Entity("CoreAPI.Data.Entity.RegistroPessoaDatasus", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("CEP")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("CNS")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("CPF")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Mae")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Municipio")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("MunicipioNascimento")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Pai")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("RGDataEmissao")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("RGNumero")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("RGOrgaoEmisor")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("RGUF")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("TelefoneSecundario")
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique()
                        .HasDatabaseName("indexCPF")
                        .HasFilter("[CPF] IS NOT NULL");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("CPF"), false);

                    b.ToTable("RegistroPessoaDatasus");
                });
#pragma warning restore 612, 618
        }
    }
}
