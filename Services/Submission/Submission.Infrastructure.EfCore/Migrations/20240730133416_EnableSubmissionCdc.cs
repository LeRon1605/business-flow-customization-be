using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Submission.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class EnableSubmissionCdc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                EXEC sys.sp_cdc_enable_db;
                GO

                EXEC sys.sp_cdc_enable_table  
	                @source_schema = N'dbo',  
	                @source_name   = N'FormSubmission',  
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
                    @source_name   = N'FormSubmission',  
                    @capture_instance = 'all';
                GO

                EXEC sys.sp_cdc_disable_db;
                GO
            ");
        }
    }
}
