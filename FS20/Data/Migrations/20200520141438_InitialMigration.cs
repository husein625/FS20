using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FS20.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompetitionType",
                columns: table => new
                {
                    CompetitionTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionType", x => x.CompetitionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePosition",
                columns: table => new
                {
                    EmployeePositionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePosition", x => x.EmployeePositionId);
                });

            migrationBuilder.CreateTable(
                name: "MembershipFee",
                columns: table => new
                {
                    MembershipFeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    amount = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipFee", x => x.MembershipFeeID);
                });

            migrationBuilder.CreateTable(
                name: "Opponent",
                columns: table => new
                {
                    OpponentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opponent", x => x.OpponentID);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    SeasonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.SeasonID);
                });

            migrationBuilder.CreateTable(
                name: "Stadium",
                columns: table => new
                {
                    StadiumID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Place = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    Telephone = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadium", x => x.StadiumID);
                });

            migrationBuilder.CreateTable(
                name: "TrainingType",
                columns: table => new
                {
                    TrainingTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingType", x => x.TrainingTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Telephone = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    EmployeePositionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_EmployeePosition_EmployeePositionID",
                        column: x => x.EmployeePositionID,
                        principalTable: "EmployeePosition",
                        principalColumn: "EmployeePositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competition",
                columns: table => new
                {
                    CompetitionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    MyProperty = table.Column<bool>(nullable: true),
                    CompetitionTypeID = table.Column<int>(nullable: true),
                    SeasonID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competition", x => x.CompetitionID);
                    table.ForeignKey(
                        name: "FK_Competition_CompetitionType_CompetitionTypeID",
                        column: x => x.CompetitionTypeID,
                        principalTable: "CompetitionType",
                        principalColumn: "CompetitionTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competition_Season_SeasonID",
                        column: x => x.SeasonID,
                        principalTable: "Season",
                        principalColumn: "SeasonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Age = table.Column<int>(nullable: true),
                    NumberOfPlayers = table.Column<int>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: true),
                    CoachID = table.Column<int>(nullable: true),
                    AssistantCoach1ID = table.Column<int>(nullable: true),
                    AssistantCoach2ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupID);
                    table.ForeignKey(
                        name: "FK_Group_Employee_AssistantCoach1ID",
                        column: x => x.AssistantCoach1ID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Group_Employee_AssistantCoach2ID",
                        column: x => x.AssistantCoach2ID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Group_Employee_CoachID",
                        column: x => x.CoachID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    UniqueId = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTimeOffset>(nullable: false),
                    MotherName = table.Column<string>(maxLength: 50, nullable: false),
                    MotherTelephone = table.Column<int>(nullable: false),
                    MotherEmail = table.Column<string>(maxLength: 50, nullable: false),
                    FatherName = table.Column<string>(maxLength: 50, nullable: false),
                    FatherTelephone = table.Column<int>(nullable: false),
                    FatherEmail = table.Column<string>(maxLength: 50, nullable: false),
                    RegistrationNumber = table.Column<int>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTimeOffset>(nullable: true),
                    GroupID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Player_Group_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    TrainingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    GroupID = table.Column<int>(nullable: true),
                    TrainingTypeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.TrainingID);
                    table.ForeignKey(
                        name: "FK_Training_Group_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Training_TrainingType_TrainingTypeID",
                        column: x => x.TrainingTypeID,
                        principalTable: "TrainingType",
                        principalColumn: "TrainingTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerMembershipFee",
                columns: table => new
                {
                    PlayerMembershipFeeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PlayerID = table.Column<int>(nullable: false),
                    MembershipFeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMembershipFee", x => x.PlayerMembershipFeeID);
                    table.ForeignKey(
                        name: "FK_PlayerMembershipFee_MembershipFee_MembershipFeeID",
                        column: x => x.MembershipFeeID,
                        principalTable: "MembershipFee",
                        principalColumn: "MembershipFeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerMembershipFee_Player_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingsPresence",
                columns: table => new
                {
                    TrainingsPresenceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: true),
                    PayerID = table.Column<int>(nullable: true),
                    PlayerId = table.Column<int>(nullable: true),
                    CoachID = table.Column<int>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    TrainingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingsPresence", x => x.TrainingsPresenceID);
                    table.ForeignKey(
                        name: "FK_TrainingsPresence_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingsPresence_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingsPresence_Training_TrainingID",
                        column: x => x.TrainingID,
                        principalTable: "Training",
                        principalColumn: "TrainingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competition_CompetitionTypeID",
                table: "Competition",
                column: "CompetitionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Competition_SeasonID",
                table: "Competition",
                column: "SeasonID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeePositionID",
                table: "Employee",
                column: "EmployeePositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_AssistantCoach1ID",
                table: "Group",
                column: "AssistantCoach1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_AssistantCoach2ID",
                table: "Group",
                column: "AssistantCoach2ID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_CoachID",
                table: "Group",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_GroupID",
                table: "Player",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMembershipFee_MembershipFeeID",
                table: "PlayerMembershipFee",
                column: "MembershipFeeID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMembershipFee_PlayerID",
                table: "PlayerMembershipFee",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Training_GroupID",
                table: "Training",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Training_TrainingTypeID",
                table: "Training",
                column: "TrainingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingsPresence_EmployeeId",
                table: "TrainingsPresence",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingsPresence_PlayerId",
                table: "TrainingsPresence",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingsPresence_TrainingID",
                table: "TrainingsPresence",
                column: "TrainingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Competition");

            migrationBuilder.DropTable(
                name: "Opponent");

            migrationBuilder.DropTable(
                name: "PlayerMembershipFee");

            migrationBuilder.DropTable(
                name: "Stadium");

            migrationBuilder.DropTable(
                name: "TrainingsPresence");

            migrationBuilder.DropTable(
                name: "CompetitionType");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "MembershipFee");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "TrainingType");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "EmployeePosition");
        }
    }
}
