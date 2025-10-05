using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZBUKSWALKS.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fileDesription",
                table: "Images",
                newName: "fileDescription");

            migrationBuilder.AddColumn<string>(
                name: "fileExtension",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fileExtension",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "fileDescription",
                table: "Images",
                newName: "fileDesription");
        }
    }
}
