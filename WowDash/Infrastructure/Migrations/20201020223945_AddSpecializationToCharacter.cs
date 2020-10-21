using Microsoft.EntityFrameworkCore.Migrations;

namespace WowDash.Infrastructure.Migrations
{
    public partial class AddSpecializationToCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Characters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Characters");
        }
    }
}
