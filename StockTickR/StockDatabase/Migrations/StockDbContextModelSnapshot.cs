// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StockDatabase.Repositories;

namespace StockDatabase.Migrations {
    [DbContext (typeof (StockDbContext))]
    partial class StockDbContextModelSnapshot : ModelSnapshot {
        protected override void BuildModel (ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema ("dbs")
                .HasAnnotation ("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation ("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation ("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity ("StockDatabase.Models.Stock", b => {
                b.Property<int> ("Id")
                    .ValueGeneratedOnAdd ();

                b.Property<decimal> ("DayHigh");

                b.Property<decimal> ("DayLow");

                b.Property<decimal> ("DayOpen");

                b.Property<decimal> ("LastChange");

                b.Property<decimal> ("Price");

                b.Property<string> ("Symbol");

                b.HasKey ("Id");

                b.ToTable ("Stocks");
            });
#pragma warning restore 612, 618
        }
    }
}