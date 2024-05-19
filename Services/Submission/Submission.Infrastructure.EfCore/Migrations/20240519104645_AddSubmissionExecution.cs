using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Submission.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddSubmissionExecution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExecutionId",
                table: "FormSubmission",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecutionId",
                table: "FormSubmission");
        }
    }
}
