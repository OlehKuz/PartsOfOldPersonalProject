using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Persistance.Migrations
{
    public partial class LoggingInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventTasksPlan",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTasksPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogEntryOfDeploymentPlanExecution",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(nullable: false),
                    UnderlyingEntityId = table.Column<int>(nullable: false),
                    LockedBy = table.Column<Guid>(nullable: true),
                    ProcessState = table.Column<int>(nullable: false),
                    CompleteByUtc = table.Column<DateTime>(nullable: true),
                    FailureCount = table.Column<int>(nullable: false),
                    EventTasksPlanId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntryOfDeploymentPlanExecution", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_LogEntryOfDeploymentPlanExecution_EventTasksPlan_EventTasksP~",
                        column: x => x.EventTasksPlanId,
                        principalTable: "EventTasksPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StageLog",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FunctionToCall = table.Column<string>(nullable: true),
                    FunctionToRestore = table.Column<string>(nullable: true),
                    Input = table.Column<string>(nullable: true),
                    Output = table.Column<string>(nullable: true),
                    ShouldWaitForPreviousStageToComplete = table.Column<bool>(nullable: false),
                    ProcessState = table.Column<int>(nullable: false),
                    EventTasksPlanId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StageLog_EventTasksPlan_EventTasksPlanId",
                        column: x => x.EventTasksPlanId,
                        principalTable: "EventTasksPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogEntryOfDeploymentPlanExecution_EventTasksPlanId",
                table: "LogEntryOfDeploymentPlanExecution",
                column: "EventTasksPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_StageLog_EventTasksPlanId",
                table: "StageLog",
                column: "EventTasksPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEntryOfDeploymentPlanExecution");

            migrationBuilder.DropTable(
                name: "StageLog");

            migrationBuilder.DropTable(
                name: "EventTasksPlan");
        }
    }
}
