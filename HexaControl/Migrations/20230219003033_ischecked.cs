using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HexaControl.Migrations
{
    public partial class ischecked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Star",
                table: "Blogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PubDate",
                table: "Commons",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "isChecked",
                table: "Blogs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isChecked",
                table: "Blogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PubDate",
                table: "Commons",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Star",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
