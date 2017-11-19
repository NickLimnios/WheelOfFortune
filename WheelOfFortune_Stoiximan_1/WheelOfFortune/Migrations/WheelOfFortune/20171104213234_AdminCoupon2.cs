using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WheelOfFortune.Migrations.WheelOfFortune
{
    public partial class AdminCoupon2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "AdminCoupon",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "AdminCoupon",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
