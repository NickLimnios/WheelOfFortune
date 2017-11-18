using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WheelOfFortune.Migrations
{
    public partial class UserBanned_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserActive",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "UserBanned",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserBanned",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "UserActive",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }
    }
}
