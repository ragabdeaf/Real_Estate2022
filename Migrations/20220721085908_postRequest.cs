using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Estate.Migrations
{
    public partial class postRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostRequests",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    custEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostRequests", x => x.id);
                    table.ForeignKey(
                        name: "FK_PostRequests_post_postid",
                        column: x => x.postid,
                        principalTable: "post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostRequests_postid",
                table: "PostRequests",
                column: "postid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostRequests");
        }
    }
}
