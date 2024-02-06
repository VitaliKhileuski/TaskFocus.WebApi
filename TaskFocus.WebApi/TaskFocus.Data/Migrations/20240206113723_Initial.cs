using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFocus.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    HashedPassword = table.Column<string>(type: "text", nullable: true),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskManagerUserSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StrictModelEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskManagerUserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskManagerUserSettings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPriorityLevelEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskManagerUserSettingsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPriorityLevelEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPriorityLevelEntity_TaskManagerUserSettings_TaskManager~",
                        column: x => x.TaskManagerUserSettingsId,
                        principalTable: "TaskManagerUserSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTaskLabelEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskManagerUserSettingsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTaskLabelEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTaskLabelEntity_TaskManagerUserSettings_TaskManagerUser~",
                        column: x => x.TaskManagerUserSettingsId,
                        principalTable: "TaskManagerUserSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PriorityId = table.Column<Guid>(type: "uuid", nullable: false),
                    LabelId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_UserPriorityLevelEntity_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "UserPriorityLevelEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Tasks_UserTaskLabelEntity_LabelId",
                        column: x => x.LabelId,
                        principalTable: "UserTaskLabelEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskManagerUserSettings_UserId",
                table: "TaskManagerUserSettings",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_LabelId",
                table: "Tasks",
                column: "LabelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PriorityId",
                table: "Tasks",
                column: "PriorityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPriorityLevelEntity_TaskManagerUserSettingsId",
                table: "UserPriorityLevelEntity",
                column: "TaskManagerUserSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaskLabelEntity_TaskManagerUserSettingsId",
                table: "UserTaskLabelEntity",
                column: "TaskManagerUserSettingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "UserPriorityLevelEntity");

            migrationBuilder.DropTable(
                name: "UserTaskLabelEntity");

            migrationBuilder.DropTable(
                name: "TaskManagerUserSettings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
