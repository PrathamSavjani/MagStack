using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagStack.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removeSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sales",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sales",
                table: "Products",
                type: "int",
                nullable: true);
        }
    }
}
