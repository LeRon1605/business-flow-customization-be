using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Submission.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormVersion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpaceId = table.Column<int>(type: "int", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormVersion_Form_FormId",
                        column: x => x.FormId,
                        principalTable: "Form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormElement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<double>(type: "float", nullable: false),
                    FormVersionId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormElement_FormVersion_FormVersionId",
                        column: x => x.FormVersionId,
                        principalTable: "FormVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormElementSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormElementSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormElementSetting_FormElement_FormElementId",
                        column: x => x.FormElementId,
                        principalTable: "FormElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OptionFormElementSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionFormElementSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionFormElementSetting_FormElement_FormElementId",
                        column: x => x.FormElementId,
                        principalTable: "FormElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormElement_FormVersionId",
                table: "FormElement",
                column: "FormVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_FormElementSetting_FormElementId",
                table: "FormElementSetting",
                column: "FormElementId");

            migrationBuilder.CreateIndex(
                name: "IX_FormVersion_FormId",
                table: "FormVersion",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionFormElementSetting_FormElementId",
                table: "OptionFormElementSetting",
                column: "FormElementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormElementSetting");

            migrationBuilder.DropTable(
                name: "OptionFormElementSetting");

            migrationBuilder.DropTable(
                name: "FormElement");

            migrationBuilder.DropTable(
                name: "FormVersion");

            migrationBuilder.DropTable(
                name: "Form");
        }
    }
}
