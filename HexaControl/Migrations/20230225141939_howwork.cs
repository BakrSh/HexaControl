using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HexaControl.Migrations
{
    public partial class howwork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubSecText",
                table: "HowWorks");

            migrationBuilder.AddColumn<string>(
                name: "SubSecText",
                table: "howWeWorks",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PubDate",
                table: "Blogs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubSecText",
                table: "howWeWorks");

            migrationBuilder.AddColumn<string>(
                name: "SubSecText",
                table: "HowWorks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PubDate",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
