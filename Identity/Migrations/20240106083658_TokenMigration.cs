using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Migrations
{
    /// <inheritdoc />
    public partial class TokenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiredDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "Token", "TokenExpiredDate" },
                values: new object[] { "0e8e4f8f-b762-4f3d-abd9-24319dd6d9c9", "AQAAAAIAAYagAAAAEO5ivzB+2pfK5Z5+qbLpLp9ChRJ5wB7llYiDIeUPtjv8GuRdz6Vj3pVJpItQnhQo6g==", "60d78e74-3882-4333-be17-c1f99afef8c6", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TokenExpiredDate",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27d3275f-27a4-44b7-8562-476747159f64", "AQAAAAIAAYagAAAAEPpY/x1/ssVMw02V5N0L6ealmJxcjJHyi249VwfFbx/Tcv/4TzlcHaB+Kxd+gMnwVA==", "5ce915d9-867c-4b8f-b435-f3b58daa12a8" });
        }
    }
}
