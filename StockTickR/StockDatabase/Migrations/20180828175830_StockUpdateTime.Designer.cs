// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockProcessor.Repositories;

namespace StockProcessor.Migrations {
    [DbContext (typeof (StockDbContext))]
    [Migration ("20180828175830_StockUpdateTime")]
    partial class StockUpdateTime {
        protected override void BuildTargetModel (ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema ("dbs")
                .HasAnnotation ("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation ("Relational:MaxIdentifierLength", 128)
                .HasAnnotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity ("StockProcessor.Models.Stock", b => {
                b.Property<int> ("Id")
                    .ValueGeneratedOnAdd ()
                    .HasAnnotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<decimal> ("DayHigh")
                    .HasColumnType ("decimal(10, 2)");

                b.Property<decimal> ("DayLow")
                    .HasColumnType ("decimal(10, 2)");

                b.Property<decimal> ("DayOpen")
                    .HasColumnType ("decimal(10, 2)");

                b.Property<decimal> ("LastChange")
                    .HasColumnType ("decimal(10, 2)");

                b.Property<decimal> ("Price")
                    .HasColumnType ("decimal(10, 2)");

                b.Property<string> ("Symbol")
                    .HasColumnType ("varchar(256)");

                b.Property<DateTime> ("UpdateTime")
                    .HasColumnType ("datetime2(7)");

                b.HasKey ("Id");

                b.ToTable ("Stocks");
            });
#pragma warning restore 612, 618
        }
    }
}