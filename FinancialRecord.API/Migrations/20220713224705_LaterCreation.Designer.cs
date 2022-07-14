﻿// <auto-generated />
using System;
using FinancialRecord.API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinancialRecord.API.Migrations
{
    [DbContext(typeof(ExperianDBContext))]
    [Migration("20220713224705_LaterCreation")]
    partial class LaterCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FinancialRecord.API.Models.CustomerInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CustomerInfos");
                });

            modelBuilder.Entity("FinancialRecord.API.Models.FinancialInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<int>("CustomerInfoId")
                        .HasColumnType("int");

                    b.Property<double>("InitialAmount")
                        .HasColumnType("float");

                    b.Property<double>("InitialTerm")
                        .HasColumnType("float");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("MinimumPaymentAmount")
                        .HasColumnType("float");

                    b.Property<double>("RemainingAmount")
                        .HasColumnType("float");

                    b.Property<double>("RemainingTerm")
                        .HasColumnType("float");

                    b.Property<double>("RepaymentAmount")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("TotalPaymentAmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerInfoId");

                    b.ToTable("FinancialInfos");
                });

            modelBuilder.Entity("FinancialRecord.API.Models.FinancialInfo", b =>
                {
                    b.HasOne("FinancialRecord.API.Models.CustomerInfo", "CustomerInfo")
                        .WithMany()
                        .HasForeignKey("CustomerInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
