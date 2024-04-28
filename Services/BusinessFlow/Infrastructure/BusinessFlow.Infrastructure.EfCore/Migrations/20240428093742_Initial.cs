using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessFlow.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Space",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Space", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpaceRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessFlowVersion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpaceId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessFlowVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessFlowVersion_Space_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Space",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpaceMember",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    SpaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceMember", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_SpaceMember_SpaceRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SpaceRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpaceMember_Space_SpaceId",
                        column: x => x.SpaceId,
                        principalTable: "Space",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessFlowBlock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessFlowVersionId = table.Column<int>(type: "int", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessFlowBlock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessFlowBlock_BusinessFlowVersion_BusinessFlowVersionId",
                        column: x => x.BusinessFlowVersionId,
                        principalTable: "BusinessFlowVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessFlowBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromBlockId = table.Column<int>(type: "int", nullable: false),
                    ToBlockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessFlowBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessFlowBranch_BusinessFlowBlock_FromBlockId",
                        column: x => x.FromBlockId,
                        principalTable: "BusinessFlowBlock",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusinessFlowBranch_BusinessFlowBlock_ToBlockId",
                        column: x => x.ToBlockId,
                        principalTable: "BusinessFlowBlock",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusinessFlowOutCome",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessFlowOutCome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessFlowOutCome_BusinessFlowBranch_Id",
                        column: x => x.Id,
                        principalTable: "BusinessFlowBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionExecution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessFlowBlockId = table.Column<int>(type: "int", nullable: false),
                    SubmissionId = table.Column<int>(type: "int", nullable: false),
                    OutComeId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionExecution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionExecution_BusinessFlowBlock_BusinessFlowBlockId",
                        column: x => x.BusinessFlowBlockId,
                        principalTable: "BusinessFlowBlock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubmissionExecution_BusinessFlowOutCome_OutComeId",
                        column: x => x.OutComeId,
                        principalTable: "BusinessFlowOutCome",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFlowBlock_BusinessFlowVersionId",
                table: "BusinessFlowBlock",
                column: "BusinessFlowVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFlowBranch_FromBlockId",
                table: "BusinessFlowBranch",
                column: "FromBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFlowBranch_ToBlockId",
                table: "BusinessFlowBranch",
                column: "ToBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFlowVersion_SpaceId",
                table: "BusinessFlowVersion",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceMember_RoleId",
                table: "SpaceMember",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceMember_SpaceId",
                table: "SpaceMember",
                column: "SpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionExecution_BusinessFlowBlockId",
                table: "SubmissionExecution",
                column: "BusinessFlowBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionExecution_OutComeId",
                table: "SubmissionExecution",
                column: "OutComeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpaceMember");

            migrationBuilder.DropTable(
                name: "SubmissionExecution");

            migrationBuilder.DropTable(
                name: "SpaceRole");

            migrationBuilder.DropTable(
                name: "BusinessFlowOutCome");

            migrationBuilder.DropTable(
                name: "BusinessFlowBranch");

            migrationBuilder.DropTable(
                name: "BusinessFlowBlock");

            migrationBuilder.DropTable(
                name: "BusinessFlowVersion");

            migrationBuilder.DropTable(
                name: "Space");
        }
    }
}
