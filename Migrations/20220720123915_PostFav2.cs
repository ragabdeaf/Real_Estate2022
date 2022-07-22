using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Estate.Migrations
{
    public partial class PostFav2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "postFavs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "postFavs");
        }
    }
}
