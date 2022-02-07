﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Transfer.Infra.Data.Context;

namespace Transfer.Infra.Migrations
{
    [DbContext(typeof(TransferContext))]
    [Migration("20220206210446_Transfer")]
    partial class Transfer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Transfer.Domain.Transfer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount")
                        .HasColumnName("Amount")
                        .HasColumnType("decimal(5,2)");

                    b.Property<DateTime>("ExpectedOn")
                        .HasColumnName("ExpectedOn")
                        .HasColumnType("DateTime");

                    b.Property<Guid>("ExternalID")
                        .HasColumnName("ExternalID")
                        .HasColumnType("uniqueIdentifier");

                    b.Property<int>("Status")
                        .HasColumnName("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("HistoryTransfers");
                });
#pragma warning restore 612, 618
        }
    }
}
