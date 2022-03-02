using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrumBot.Migrations
{
    public partial class AddedDrumTaskAndRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrumTasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Page = table.Column<int>(type: "int", nullable: true),
                    TaskNumber = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrumTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JournalWrites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxBpm = table.Column<int>(type: "int", nullable: false),
                    MinutesSpent = table.Column<int>(type: "int", nullable: false),
                    DrumTaskId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalWrites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalWrites_DrumTasks_DrumTaskId",
                        column: x => x.DrumTaskId,
                        principalTable: "DrumTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalWrites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalWrites_DrumTaskId",
                table: "JournalWrites",
                column: "DrumTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalWrites_UserId",
                table: "JournalWrites",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalWrites");

            migrationBuilder.DropTable(
                name: "DrumTasks");
        }
    }
}
