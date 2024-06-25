using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessFlow.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddSpacePermissionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpacePermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpacePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpacePermission_SpaceRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SpaceRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpacePermission_RoleId",
                table: "SpacePermission",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpacePermission");
        }
    }
}
