using Microsoft.EntityFrameworkCore.Migrations;

namespace FS20.Data.Migrations
{
    public partial class AspNetUserIdToPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AspNetUserID",
                table: "Player",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Player_AspNetUserID",
                table: "Player",
                column: "AspNetUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_AspNetUsers_AspNetUserID",
                table: "Player",
                column: "AspNetUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_AspNetUsers_AspNetUserID",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_AspNetUserID",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "AspNetUserID",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
