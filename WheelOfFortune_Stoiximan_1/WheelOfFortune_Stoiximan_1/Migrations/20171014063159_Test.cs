using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WheelOfFortune_Stoiximan_1.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pceu_Coupons_CouponId",
                table: "Pceu");

            migrationBuilder.DropIndex(
                name: "IX_Pceu_CouponId",
                table: "Pceu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pceu_CouponId",
                table: "Pceu",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pceu_Coupons_CouponId",
                table: "Pceu",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
