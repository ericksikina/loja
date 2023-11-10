﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using aplicacaoLoja.Models;

#nullable disable

namespace aplicacaoLoja.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20231110133059_CorrecaoTelefone3")]
    partial class CorrecaoTelefone3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("aplicacaoLoja.Models.Categoria", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("aplicacaoLoja.Models.Cliente", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("telefone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("aplicacaoLoja.Models.CompraProduto", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("produtoID")
                        .HasColumnType("int");

                    b.Property<int>("qtde")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("produtoID");

                    b.ToTable("CompraProdutos");
                });

            modelBuilder.Entity("aplicacaoLoja.Models.Fornecedor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("cnpj")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("endereco")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("telefone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("id");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("aplicacaoLoja.Models.Funcionario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("salario")
                        .HasColumnType("float");

                    b.Property<string>("telefone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.HasKey("id");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("aplicacaoLoja.Models.Produto", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("categoriaID")
                        .HasColumnType("int");

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("fornecedorID")
                        .HasColumnType("int");

                    b.Property<decimal>("preco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("qtdeEstoque")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("categoriaID");

                    b.HasIndex("fornecedorID");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("aplicacaoLoja.Models.Venda", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("clienteID")
                        .HasColumnType("int");

                    b.Property<DateTime>("data")
                        .HasColumnType("datetime2");

                    b.Property<int>("funcionarioID")
                        .HasColumnType("int");

                    b.Property<int>("produtoID")
                        .HasColumnType("int");

                    b.Property<int>("quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.HasIndex("clienteID");

                    b.HasIndex("funcionarioID");

                    b.HasIndex("produtoID");

                    b.ToTable("Vendas");
                });

            modelBuilder.Entity("aplicacaoLoja.Models.CompraProduto", b =>
                {
                    b.HasOne("aplicacaoLoja.Models.Produto", "produto")
                        .WithMany()
                        .HasForeignKey("produtoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("produto");
                });

            modelBuilder.Entity("aplicacaoLoja.Models.Produto", b =>
                {
                    b.HasOne("aplicacaoLoja.Models.Categoria", "categoria")
                        .WithMany()
                        .HasForeignKey("categoriaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aplicacaoLoja.Models.Fornecedor", "fornecedor")
                        .WithMany()
                        .HasForeignKey("fornecedorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("categoria");

                    b.Navigation("fornecedor");
                });

            modelBuilder.Entity("aplicacaoLoja.Models.Venda", b =>
                {
                    b.HasOne("aplicacaoLoja.Models.Cliente", "cliente")
                        .WithMany()
                        .HasForeignKey("clienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aplicacaoLoja.Models.Funcionario", "funcionario")
                        .WithMany()
                        .HasForeignKey("funcionarioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aplicacaoLoja.Models.Produto", "produto")
                        .WithMany()
                        .HasForeignKey("produtoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cliente");

                    b.Navigation("funcionario");

                    b.Navigation("produto");
                });
#pragma warning restore 612, 618
        }
    }
}
