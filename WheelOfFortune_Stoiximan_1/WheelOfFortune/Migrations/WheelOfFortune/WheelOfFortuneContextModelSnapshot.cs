﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WheelOfFortune.Models;

namespace WheelOfFortune.Migrations.WheelOfFortune
{
    [DbContext(typeof(WheelOfFortuneContext))]
    partial class WheelOfFortuneContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WheelOfFortune.Models.AdminCoupon", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Status");

                    b.Property<int?>("Value");

                    b.HasKey("ID");

                    b.ToTable("AdminCoupon");
                });
#pragma warning restore 612, 618
        }
    }
}
