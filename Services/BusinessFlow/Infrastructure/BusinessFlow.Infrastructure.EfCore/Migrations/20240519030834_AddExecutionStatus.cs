using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessFlow.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddExecutionStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SubmissionExecution",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubmissionExecution");
        }
    }
}
