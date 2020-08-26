using Microsoft.EntityFrameworkCore.Migrations;

namespace WowDash.Infrastructure.Migrations
{
    public partial class ModifyTaskAndTaskCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTodaysGoal",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TaskCharacters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Subclass",
                table: "GameDataReference",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TaskCharacters");

            migrationBuilder.DropColumn(
                name: "Subclass",
                table: "GameDataReference");

            migrationBuilder.AddColumn<bool>(
                name: "IsTodaysGoal",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
