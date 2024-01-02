using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 2, 11, 59, 53, 945, DateTimeKind.Local).AddTicks(9756), "description1", new DateTime(2024, 1, 2, 11, 59, 53, 945, DateTimeKind.Local).AddTicks(9756), "name1" },
                    { 2, new DateTime(2024, 1, 2, 11, 59, 53, 945, DateTimeKind.Local).AddTicks(9756), "description2", new DateTime(2024, 1, 2, 11, 59, 53, 945, DateTimeKind.Local).AddTicks(9756), "name2" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "ExpirationDate", "ModifiedDate", "Name", "Price", "Weight" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 2, 11, 59, 53, 946, DateTimeKind.Local).AddTicks(1468), "description1", new DateTime(2025, 1, 2, 11, 59, 53, 946, DateTimeKind.Local).AddTicks(1468), new DateTime(2024, 1, 2, 11, 59, 53, 946, DateTimeKind.Local).AddTicks(1468), "name1", 10000m, 1000 },
                    { 2, 2, new DateTime(2024, 1, 2, 11, 59, 53, 946, DateTimeKind.Local).AddTicks(1468), "description2", new DateTime(2026, 1, 2, 11, 59, 53, 946, DateTimeKind.Local).AddTicks(1468), new DateTime(2024, 1, 2, 11, 59, 53, 946, DateTimeKind.Local).AddTicks(1468), "name2", 20000m, 2000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
