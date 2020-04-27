using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI.DataEFCoreSQL.Migrations
{
    public partial class Apply_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Administrador" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Trabajador" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birthdate", "Name", "RoleId", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(1972, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jesús", 1, "Sánchez" },
                    { 2, new DateTime(1971, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rubén", 1, "Morales" },
                    { 5, new DateTime(2000, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pedro", 1, "González" },
                    { 3, new DateTime(1990, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ariadna", 2, "Gonzáelz" },
                    { 4, new DateTime(2008, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daniela", 2, "Aceituno" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
