﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TomatoAPI;

namespace TomatoAPI.Migrations
{
    [DbContext(typeof(TomatoDb))]
    [Migration("20180104171912_First Migration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.5");

            modelBuilder.Entity("TomatoAPI.Tomato", b =>
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
