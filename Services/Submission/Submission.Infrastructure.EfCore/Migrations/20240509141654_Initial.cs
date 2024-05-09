using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Submission.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormSubmission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormVersionId = table.Column<int>(type: "int", nullable: false),
                    BusinessFlowVersionId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormSubmission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpaceBusinessFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpaceId = table.Column<int>(type: "int", nullable: false),
                    BusinessFlowVersionId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceBusinessFlow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionAttachmentField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionId = table.Column<int>(type: "int", nullable: false),
                    ElementId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionAttachmentField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionAttachmentField_FormSubmission_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "FormSubmission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionDateValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionId = table.Column<int>(type: "int", nullable: false),
                    ElementId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionDateValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionDateValue_FormSubmission_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "FormSubmission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionNumberValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionId = table.Column<int>(type: "int", nullable: false),
                    ElementId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionNumberValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionNumberValue_FormSubmission_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "FormSubmission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionOptionField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionId = table.Column<int>(type: "int", nullable: false),
                    ElementId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionOptionField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionOptionField_FormSubmission_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "FormSubmission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionTextValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionId = table.Column<int>(type: "int", nullable: false),
                    ElementId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionTextValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionTextValue_FormSubmission_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "FormSubmission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionAttachmentValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionFieldId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionAttachmentValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionAttachmentValue_SubmissionAttachmentField_SubmissionFieldId",
                        column: x => x.SubmissionFieldId,
                        principalTable: "SubmissionAttachmentField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionOptionFieldValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionFieldId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionOptionFieldValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmissionOptionFieldValue_SubmissionOptionField_SubmissionFieldId",
                        column: x => x.SubmissionFieldId,
                        principalTable: "SubmissionOptionField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionAttachmentField_SubmissionId",
                table: "SubmissionAttachmentField",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionAttachmentValue_SubmissionFieldId",
                table: "SubmissionAttachmentValue",
                column: "SubmissionFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionDateValue_SubmissionId",
                table: "SubmissionDateValue",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionNumberValue_SubmissionId",
                table: "SubmissionNumberValue",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionOptionField_SubmissionId",
                table: "SubmissionOptionField",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionOptionFieldValue_SubmissionFieldId",
                table: "SubmissionOptionFieldValue",
                column: "SubmissionFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionTextValue_SubmissionId",
                table: "SubmissionTextValue",
                column: "SubmissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpaceBusinessFlow");

            migrationBuilder.DropTable(
                name: "SubmissionAttachmentValue");

            migrationBuilder.DropTable(
                name: "SubmissionDateValue");

            migrationBuilder.DropTable(
                name: "SubmissionNumberValue");

            migrationBuilder.DropTable(
                name: "SubmissionOptionFieldValue");

            migrationBuilder.DropTable(
                name: "SubmissionTextValue");

            migrationBuilder.DropTable(
                name: "SubmissionAttachmentField");

            migrationBuilder.DropTable(
                name: "SubmissionOptionField");

            migrationBuilder.DropTable(
                name: "FormSubmission");
        }
    }
}
