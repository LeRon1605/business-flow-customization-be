using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessFlow.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class EnableSpaceCdc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                EXEC sys.sp_cdc_enable_db;
                GO

                EXEC sys.sp_cdc_enable_table  
	                @source_schema = N'dbo',  
	                @source_name   = N'Space',  
	                @role_name     = NULL,  
	                @filegroup_name = NULL,  
	                @supports_net_changes = 1;
                GO
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                EXEC sys.sp_cdc_disable_table  
                    @source_schema = N'dbo',  
                    @source_name   = N'Space',  
                    @capture_instance = 'all';
                GO

                EXEC sys.sp_cdc_disable_db;
            ");
        }
    }
}
