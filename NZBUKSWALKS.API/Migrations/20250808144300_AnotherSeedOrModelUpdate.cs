using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZBUKSWALKS.API.Migrations
{
    /// <inheritdoc />
    public partial class AnotherSeedOrModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "id",
                keyValue: new Guid("425c1e7e-d69d-4ccc-85fd-293319e24741"),
                column: "name",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "id",
                keyValue: new Guid("dc57473a-1790-4efc-8db5-8a217a7ed37c"),
                column: "name",
                value: "Hard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "id",
                keyValue: new Guid("425c1e7e-d69d-4ccc-85fd-293319e24741"),
                column: "name",
                value: "");

            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "id",
                keyValue: new Guid("dc57473a-1790-4efc-8db5-8a217a7ed37c"),
                column: "name",
                value: "");
        }
    }
}
