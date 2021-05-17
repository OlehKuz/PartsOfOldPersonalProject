using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Persistance.Migrations
{
    public partial class LoggingInitial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogEntryOfDeploymentPlanExecution_EventTasksPlan_EventTasksP~",
                table: "LogEntryOfDeploymentPlanExecution");

            migrationBuilder.DropForeignKey(
                name: "FK_StageLog_EventTasksPlan_EventTasksPlanId",
                table: "StageLog");

            migrationBuilder.DropTable(
                name: "EventTasksPlan");

            migrationBuilder.DropIndex(
                name: "IX_StageLog_EventTasksPlanId",
                table: "StageLog");

            migrationBuilder.DropIndex(
                name: "IX_LogEntryOfDeploymentPlanExecution_EventTasksPlanId",
                table: "LogEntryOfDeploymentPlanExecution");

            migrationBuilder.DropColumn(
                name: "EventTasksPlanId",
                table: "StageLog");

            migrationBuilder.DropColumn(
                name: "EventTasksPlanId",
                table: "LogEntryOfDeploymentPlanExecution");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskDeploymentLogEntryTransactionId",
                table: "StageLog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventName",
                table: "LogEntryOfDeploymentPlanExecution",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StageLog_TaskDeploymentLogEntryTransactionId",
                table: "StageLog",
                column: "TaskDeploymentLogEntryTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_StageLog_LogEntryOfDeploymentPlanExecution_TaskDeploymentLog~",
                table: "StageLog",
                column: "TaskDeploymentLogEntryTransactionId",
                principalTable: "LogEntryOfDeploymentPlanExecution",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StageLog_LogEntryOfDeploymentPlanExecution_TaskDeploymentLog~",
                table: "StageLog");

            migrationBuilder.DropIndex(
                name: "IX_StageLog_TaskDeploymentLogEntryTransactionId",
                table: "StageLog");

            migrationBuilder.DropColumn(
                name: "TaskDeploymentLogEntryTransactionId",
                table: "StageLog");

            migrationBuilder.DropColumn(
                name: "EventName",
                table: "LogEntryOfDeploymentPlanExecution");

            migrationBuilder.AddColumn<string>(
                name: "EventTasksPlanId",
                table: "StageLog",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventTasksPlanId",
                table: "LogEntryOfDeploymentPlanExecution",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventTasksPlan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTasksPlan", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StageLog_EventTasksPlanId",
                table: "StageLog",
                column: "EventTasksPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntryOfDeploymentPlanExecution_EventTasksPlanId",
                table: "LogEntryOfDeploymentPlanExecution",
                column: "EventTasksPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogEntryOfDeploymentPlanExecution_EventTasksPlan_EventTasksP~",
                table: "LogEntryOfDeploymentPlanExecution",
                column: "EventTasksPlanId",
                principalTable: "EventTasksPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StageLog_EventTasksPlan_EventTasksPlanId",
                table: "StageLog",
                column: "EventTasksPlanId",
                principalTable: "EventTasksPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
