using Microsoft.EntityFrameworkCore.Migrations;

namespace WowDash.Infrastructure.Migrations
{
    public partial class NullableDefaultTaskType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DefaultTaskType",
                table: "Players",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DefaultTaskType",
                table: "Players",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
