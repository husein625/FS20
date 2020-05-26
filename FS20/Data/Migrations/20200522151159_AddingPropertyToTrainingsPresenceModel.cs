using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FS20.Data.Migrations
{
    public partial class AddingPropertyToTrainingsPresenceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingsPresence_Player_PlayerId",
                table: "TrainingsPresence");

            migrationBuilder.DropColumn(
                name: "PayerID",
                table: "TrainingsPresence");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "TrainingsPresence",
                newName: "PlayerID");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingsPresence_PlayerId",
                table: "TrainingsPresence",
                newName: "IX_TrainingsPresence_PlayerID");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TrainingsPresence",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingsPresence_Player_PlayerID",
                table: "TrainingsPresence",
                column: "PlayerID",
                principalTable: "Player",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingsPresence_Player_PlayerID",
                table: "TrainingsPresence");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TrainingsPresence");

            migrationBuilder.RenameColumn(
                name: "PlayerID",
                table: "TrainingsPresence",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingsPresence_PlayerID",
                table: "TrainingsPresence",
                newName: "IX_TrainingsPresence_PlayerId");

            migrationBuilder.AddColumn<int>(
                name: "PayerID",
                table: "TrainingsPresence",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingsPresence_Player_PlayerId",
                table: "TrainingsPresence",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
