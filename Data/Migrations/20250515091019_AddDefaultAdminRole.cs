using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccinationTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", null, "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c3736aaf-9e26-4116-b48a-c8ce741e9e46", "AQAAAAIAAYagAAAAEB0gpvLe2C0O6TXvOtTquOISk40ksRNRJHxSofMkSQFV8s5qoJAhuvzqWe+sUcMQQg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f9a2b8d5-6021-4636-a5ed-bd9beb0f264f", "AQAAAAIAAYagAAAAEEHnYX8zcThWSOLqZnjiU3VXYf378tuFrWSunPDnO699qETxHfkb2cLq45r+tfYCbQ==" });
        }
    }
}
