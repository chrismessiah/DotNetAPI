using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TomatoAPI.Data;
using TomatoAPI.Models;

namespace TomatoAPI.Migrations
{
    [DbContext(typeof(TomatoDbContext))]
    partial class TomatoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.5");

            modelBuilder.Entity("TomatoAPI.Models.Tomato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("OriginPostCode");

                    b.Property<int>("Tastes");

                    b.HasKey("Id");

                    b.ToTable("Tomatos");
                });
        }
    }
}
