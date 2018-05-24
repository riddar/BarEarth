using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BarEarth.Data.Migrations
{
    public partial class BarUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bars",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bars_UserId",
                table: "Bars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bars_AspNetUsers_UserId",
                table: "Bars",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bars_AspNetUsers_UserId",
                table: "Bars");

            migrationBuilder.DropIndex(
                name: "IX_Bars_UserId",
                table: "Bars");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bars");
        }
    }
}
