using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicClassManager.Entities.Storage.Migrations
{
    /// <inheritdoc />
    public partial class AddSchoolClassPseudoName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PseudoName",
                table: "SchoolClass",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClass_PseudoName",
                table: "SchoolClass",
                column: "PseudoName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SchoolClass_PseudoName",
                table: "SchoolClass");

            migrationBuilder.DropColumn(
                name: "PseudoName",
                table: "SchoolClass");
        }
    }
}
