using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Real_Estate.Migrations
{
    public partial class postRequest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostRequests_post_postid",
                table: "PostRequests");

            migrationBuilder.RenameColumn(
                name: "postid",
                table: "PostRequests",
                newName: "postId");

            migrationBuilder.RenameIndex(
                name: "IX_PostRequests_postid",
                table: "PostRequests",
                newName: "IX_PostRequests_postId");

            migrationBuilder.AlterColumn<string>(
                name: "custEmail",
                table: "PostRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_PostRequests_post_postId",
                table: "PostRequests",
                column: "postId",
                principalTable: "post",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostRequests_post_postId",
                table: "PostRequests");

            migrationBuilder.RenameColumn(
                name: "postId",
                table: "PostRequests",
                newName: "postid");

            migrationBuilder.RenameIndex(
                name: "IX_PostRequests_postId",
                table: "PostRequests",
                newName: "IX_PostRequests_postid");

            migrationBuilder.AlterColumn<string>(
                name: "custEmail",
                table: "PostRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostRequests_post_postid",
                table: "PostRequests",
                column: "postid",
                principalTable: "post",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
