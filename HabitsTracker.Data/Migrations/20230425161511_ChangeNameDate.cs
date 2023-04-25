using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitsTracker.Data.Migrations
{
    public partial class ChangeNameDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateForm",
                table: "HabitsDictionary",
                newName: "DateFrom");

            migrationBuilder.RenameColumn(
                name: "DateForm",
                table: "Habits",
                newName: "DateFrom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateFrom",
                table: "HabitsDictionary",
                newName: "DateForm");

            migrationBuilder.RenameColumn(
                name: "DateFrom",
                table: "Habits",
                newName: "DateForm");
        }
    }
}
