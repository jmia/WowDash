using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WowDash.Infrastructure.Migrations
{
    public partial class InitialRecreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    GoogleId = table.Column<string>(nullable: true),
                    BlizzardId = table.Column<string>(nullable: true),
                    DefaultTaskType = table.Column<int>(nullable: false),
                    DefaultRealm = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    GameId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: true),
                    Class = table.Column<string>(nullable: true),
                    Race = table.Column<string>(nullable: true),
                    Realm = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<Guid>(nullable: false),
                    IsTodaysGoal = table.Column<bool>(nullable: false),
                    IsFavourite = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    TaskType = table.Column<int>(nullable: false),
                    CollectibleType = table.Column<int>(nullable: false),
                    Source = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    RefreshFrequency = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameDataReference",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<Guid>(nullable: false),
                    GameId = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDataReference", x => new { x.TaskId, x.Id });
                    table.ForeignKey(
                        name: "FK_GameDataReference_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskCharacters",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(nullable: false),
                    CharacterId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCharacters", x => new { x.CharacterId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_TaskCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskCharacters_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PlayerId",
                table: "Characters",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCharacters_TaskId",
                table: "TaskCharacters",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PlayerId",
                table: "Tasks",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameDataReference");

            migrationBuilder.DropTable(
                name: "TaskCharacters");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
