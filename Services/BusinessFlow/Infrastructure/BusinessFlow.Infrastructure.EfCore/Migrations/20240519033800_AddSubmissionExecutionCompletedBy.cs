using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessFlow.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddSubmissionExecutionCompletedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "SubmissionExecution",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompletedBy",
                table: "SubmissionExecution",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "SubmissionExecution");

            migrationBuilder.DropColumn(
                name: "CompletedBy",
                table: "SubmissionExecution");
        }
    }
}
