using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUSerConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_AspNetUsers_AppUserId",
                table: "Todos");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Todos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_AppUserId1",
                table: "Todos",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_AspNetUsers_AppUserId",
                table: "Todos",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_AspNetUsers_AppUserId1",
                table: "Todos",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_AspNetUsers_AppUserId",
                table: "Todos");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_AspNetUsers_AppUserId1",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_AppUserId1",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Todos");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_AspNetUsers_AppUserId",
                table: "Todos",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
