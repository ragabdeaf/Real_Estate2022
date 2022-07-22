using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Estate.Migrations
{
    public partial class B2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "govarnate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "country",
                table: "govarnate");
        }
    }
}
