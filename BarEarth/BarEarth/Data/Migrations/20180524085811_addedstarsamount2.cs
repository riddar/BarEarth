using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BarEarth.Data.Migrations
{
    public partial class addedstarsamount2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfStars",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "TotalRating",
                table: "Ratings");

            migrationBuilder.AddColumn<int>(
                name: "AmountOfStars",
                table: "Bars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRating",
                table: "Bars",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfStars",
                table: "Bars");

            migrationBuilder.DropColumn(
                name: "TotalRating",
                table: "Bars");

            migrationBuilder.AddColumn<int>(
                name: "AmountOfStars",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalRating",
                table: "Ratings",
                nullable: false,
                defaultValue: 0);
        }
    }
}
