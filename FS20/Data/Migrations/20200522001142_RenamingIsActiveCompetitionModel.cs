using Microsoft.EntityFrameworkCore.Migrations;

namespace FS20.Data.Migrations
{
    public partial class RenamingIsActiveCompetitionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Competition");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TrainingType",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Competition",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Competition");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TrainingType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<bool>(
                name: "MyProperty",
                table: "Competition",
                type: "bit",
                nullable: true);
        }
    }
}
