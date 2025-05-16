using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccinationTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationPropertiesToVaccinationSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "VaccinationSchedules",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.Sql(@"IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Id = '1')
            BEGIN
                INSERT INTO AspNetRoles (Id, ConcurrencyStamp, Name, NormalizedName)
                VALUES ('1', NULL, 'Admin', 'ADMIN')
            END");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8b987fca-c2ee-4429-991e-a3e564a1e77b", "AQAAAAIAAYagAAAAEHGxfGnJCg3WPZq+Uf+rq1c68YIpAymoggHnVzjuIUAfZEZJnxr/fVxvqG4ZY0hTtA==" });

            migrationBuilder.Sql(@"DELETE FROM VaccinationSchedules WHERE VaccineId NOT IN (SELECT Id FROM Vaccines)");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationSchedules_UserId",
                table: "VaccinationSchedules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinationSchedules_VaccineId",
                table: "VaccinationSchedules",
                column: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationSchedules_AspNetUsers_UserId",
                table: "VaccinationSchedules",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationSchedules_Vaccines_VaccineId",
                table: "VaccinationSchedules",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationSchedules_AspNetUsers_UserId",
                table: "VaccinationSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationSchedules_Vaccines_VaccineId",
                table: "VaccinationSchedules");

            migrationBuilder.DropIndex(
                name: "IX_VaccinationSchedules_UserId",
                table: "VaccinationSchedules");

            migrationBuilder.DropIndex(
                name: "IX_VaccinationSchedules_VaccineId",
                table: "VaccinationSchedules");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "VaccinationSchedules",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f6e8bd86-0ad2-4085-85cf-a1c67e5d1aa6", "AQAAAAIAAYagAAAAEISoWMMEtfabBNeotzwv7TNv3eD3duOgLgWIC8imJhEyKDH0LIzFY689fo5Q7JGDfQ==" });
        }
    }
}
