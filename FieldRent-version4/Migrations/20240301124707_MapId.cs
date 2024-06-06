using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldRent.Migrations
{
    /// <inheritdoc />
    public partial class MapId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MapId",
                table: "PurchaseHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHistories_MapId",
                table: "PurchaseHistories",
                column: "MapId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistories_Maps_MapId",
                table: "PurchaseHistories",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "MapId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistories_Maps_MapId",
                table: "PurchaseHistories");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHistories_MapId",
                table: "PurchaseHistories");

            migrationBuilder.DropColumn(
                name: "MapId",
                table: "PurchaseHistories");
        }
    }
}
