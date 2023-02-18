using Microsoft.EntityFrameworkCore.Migrations;

namespace HexaControl.Migrations
{
    public partial class date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "WhyHexaElements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "WhyHexaElements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
