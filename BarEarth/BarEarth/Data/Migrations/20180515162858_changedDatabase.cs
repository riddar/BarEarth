using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BarEarth.Data.Migrations
{
    public partial class changedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Bars_BarId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "FaceBook",
                table: "Bars");

            migrationBuilder.AlterColumn<string>(
                name: "Review",
                table: "Ratings",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 160,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BarId",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Bars",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Bars",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 160,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Bars_BarId",
                table: "Ratings",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Bars_BarId",
                table: "Ratings");

            migrationBuilder.AlterColumn<string>(
                name: "Review",
                table: "Ratings",
                maxLength: 160,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BarId",
                table: "Ratings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Bars",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Bars",
                maxLength: 160,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaceBook",
                table: "Bars",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Bars_BarId",
                table: "Ratings",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
