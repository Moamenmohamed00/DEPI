using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkVaultApi.Migrations
{
    /// <inheritdoc />
    public partial class urlconstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_BookMark_URL_Valid",
                table: "BookMarks",
                sql: "URL LIKE 'http://%' OR URL LIKE 'https://%' OR URL LIKE 'ftp://%'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_BookMark_URL_Valid",
                table: "BookMarks");
        }
    }
}
