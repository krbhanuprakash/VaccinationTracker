using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccinationTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatusToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Map existing string values to integers
            migrationBuilder.Sql("UPDATE VaccinationSchedules SET Status = CASE Status WHEN 'Pending' THEN 0 WHEN 'Active' THEN 1 WHEN 'Completed' THEN 2 END");

            // Alter the column to integer type
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "VaccinationSchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b27edee5-c38b-4077-890d-3c795b2e6751", "AQAAAAIAAYagAAAAEAUiFokJP+V19D7AbX1199IpzpNcZdpUXSq3dYyoRtTHJd129vCrflhdRW0vtzYNHA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert the column to string type
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "VaccinationSchedules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // Map integers back to string values
            migrationBuilder.Sql("UPDATE VaccinationSchedules SET Status = CASE Status WHEN 0 THEN 'Pending' WHEN 1 THEN 'Active' WHEN 2 THEN 'Completed' END");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8b987fca-c2ee-4429-991e-a3e564a1e77b", "AQAAAAIAAYagAAAAEHGxfGnJCg3WPZq+Uf+rq1c68YIpAymoggHnVzjuIUAfZEZJnxr/fVxvqG4ZY0hTtA==" });
        }
    }
}
