using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagStack.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sales",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sales",
                table: "Products");
        }
    }
}
