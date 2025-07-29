using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Data.Migrations
{
    public partial class SeedRoleAndUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreateDate", "DeleteDate", "Description", "Name", "NormalizedName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "bfd31bed-8846-45f4-a3f2-eb05124d9254", new DateTime(2025, 7, 10, 7, 46, 0, 0, DateTimeKind.Local), null, "SuperAdmin", "SuperAdmin", null, null },
                    { 2, "30ab66ef-de09-40c8-955f-b4483e546e5e", new DateTime(2025, 7, 10, 7, 46, 0, 0, DateTimeKind.Local), null, "Admin role", "Admin", null, null },
                    { 3, "776dd342-0ccc-4bcb-893f-181baba6745f", new DateTime(2025, 7, 10, 7, 46, 0, 0, DateTimeKind.Local), null, "Customer", "Customer", null, null }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e9bc18b0-c68b-4256-b39b-667d0407af96");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3c40bc3c-c013-480e-bd72-29d2f8efc23a");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 2, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "98f6aee8-3d5c-4a52-90f5-a32f9c7e2cb6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b4fc1f4f-a6b1-4dce-aba9-83532828df2f");
        }
    }
}
