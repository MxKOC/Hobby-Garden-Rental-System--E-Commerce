using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldRent.Migrations
{
    /// <inheritdoc />
    public partial class Shoptime2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "PurchaseHistories",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "PurchaseHistories");
        }
    }
}
