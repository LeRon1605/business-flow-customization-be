using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Submission.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFormPublicToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicLinkUrl",
                table: "Form");

            migrationBuilder.AlterColumn<string>(
                name: "PublicToken",
                table: "Form",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PublicToken",
                table: "Form",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PublicLinkUrl",
                table: "Form",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
