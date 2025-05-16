using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccinationTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class latestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3dbb7e08-85fe-4c24-90e6-ea35ae7e2662", "AQAAAAIAAYagAAAAEKApTYdCmdPeTBZQfvmKT1uKeNGUePdcnEdAuiOjochcLJdbnUnHiYZXEfOIaO1fKA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "19d23246-fca3-45a8-bdb4-0e78f142a3c9", "AQAAAAIAAYagAAAAELcrfIZmd35baH3gg3qxa7fJl4OCzVqLkOUzapyzQ4mXUjx0kbtavY9o8/Nn8ukU8Q==" });
        }
    }
}
