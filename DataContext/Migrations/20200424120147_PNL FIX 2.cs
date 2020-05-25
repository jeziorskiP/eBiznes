using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class PNLFIX2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletenessDateUserId",
                table: "PackingNotes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryNoteNumber",
                table: "DeliveryNotes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletenessDateUserId",
                table: "PackingNotes");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryNoteNumber",
                table: "DeliveryNotes",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
