using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccinationTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "f9a2b8d5-6021-4636-a5ed-bd9beb0f264f", "Admin@VT.com", true, false, null, "ADMIN@VT.COM", "ADMIN@VT.COM", "AQAAAAIAAYagAAAAEEHnYX8zcThWSOLqZnjiU3VXYf378tuFrWSunPDnO699qETxHfkb2cLq45r+tfYCbQ==", null, false, "", false, "Admin@VT.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");
        }
    }
}
