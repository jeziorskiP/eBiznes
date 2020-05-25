using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class PNLFIX3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PackingNoteLines_OrderId",
                table: "PackingNoteLines");

            migrationBuilder.DropColumn(
                name: "CompletenessDateUserId",
                table: "PackingNotes");

            migrationBuilder.CreateIndex(
                name: "IX_PackingNoteLines_OrderId",
                table: "PackingNoteLines",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PackingNoteLines_OrderId",
                table: "PackingNoteLines");

            migrationBuilder.AddColumn<int>(
                name: "CompletenessDateUserId",
                table: "PackingNotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PackingNoteLines_OrderId",
                table: "PackingNoteLines",
                column: "OrderId",
                unique: true);
        }
    }
}
