using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class PNLORDERFIX : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackingNotes_Orders_OrderId",
                table: "PackingNotes");

            migrationBuilder.DropIndex(
                name: "IX_PackingNotes_OrderId",
                table: "PackingNotes");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "PackingNotes");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "PackingNoteLines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PackingNoteLines_OrderId",
                table: "PackingNoteLines",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PackingNoteLines_Orders_OrderId",
                table: "PackingNoteLines",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackingNoteLines_Orders_OrderId",
                table: "PackingNoteLines");

            migrationBuilder.DropIndex(
                name: "IX_PackingNoteLines_OrderId",
                table: "PackingNoteLines");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "PackingNoteLines");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "PackingNotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PackingNotes_OrderId",
                table: "PackingNotes",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PackingNotes_Orders_OrderId",
                table: "PackingNotes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
