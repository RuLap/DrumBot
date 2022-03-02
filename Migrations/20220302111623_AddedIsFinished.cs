using Microsoft.EntityFrameworkCore.Migrations;

namespace DrumBot.Migrations
{
    public partial class AddedIsFinished : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "JournalWrites",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "JournalWrites");
        }
    }
}
