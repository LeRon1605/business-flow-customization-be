using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessFlow.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessFlowBlockSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessFlowBlockPersonInChargeSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessFlowBlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessFlowBlockPersonInChargeSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessFlowBlockPersonInChargeSetting_BusinessFlowBlock_BusinessFlowBlockId",
                        column: x => x.BusinessFlowBlockId,
                        principalTable: "BusinessFlowBlock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessFlowBlockTaskSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<double>(type: "float", nullable: false),
                    BusinessFlowBlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessFlowBlockTaskSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessFlowBlockTaskSetting_BusinessFlowBlock_BusinessFlowBlockId",
                        column: x => x.BusinessFlowBlockId,
                        principalTable: "BusinessFlowBlock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionExecutionPersonInCharge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionExecutionPersonInCharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionExecutionPersonInCharge_SubmissionExecution_ExecutionId",
                        column: x => x.ExecutionId,
                        principalTable: "SubmissionExecution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionExecutionTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<double>(type: "float", nullable: false),
                    ExecutionId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionExecutionTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionExecutionTask_SubmissionExecution_ExecutionId",
                        column: x => x.ExecutionId,
                        principalTable: "SubmissionExecution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFlowBlockPersonInChargeSetting_BusinessFlowBlockId",
                table: "BusinessFlowBlockPersonInChargeSetting",
                column: "BusinessFlowBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFlowBlockTaskSetting_BusinessFlowBlockId",
                table: "BusinessFlowBlockTaskSetting",
                column: "BusinessFlowBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionExecutionPersonInCharge_ExecutionId",
                table: "SubmissionExecutionPersonInCharge",
                column: "ExecutionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionExecutionTask_ExecutionId",
                table: "SubmissionExecutionTask",
                column: "ExecutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessFlowBlockPersonInChargeSetting");

            migrationBuilder.DropTable(
                name: "BusinessFlowBlockTaskSetting");

            migrationBuilder.DropTable(
                name: "SubmissionExecutionPersonInCharge");

            migrationBuilder.DropTable(
                name: "SubmissionExecutionTask");
        }
    }
}
