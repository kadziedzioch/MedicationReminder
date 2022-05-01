using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicationReminder.Web.Migrations
{
    public partial class InitialTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    MedicineId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsImmunosuppressive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.MedicineId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RemindTimes",
                columns: table => new
                {
                    RemindTimeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Dose = table.Column<int>(type: "int", nullable: false),
                    IsSelected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemindTimes", x => x.RemindTimeId);
                    table.ForeignKey(
                        name: "FK_RemindTimes_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RemindTimes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RemindTimes_MedicineId",
                table: "RemindTimes",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_RemindTimes_UserId",
                table: "RemindTimes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RemindTimes");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
