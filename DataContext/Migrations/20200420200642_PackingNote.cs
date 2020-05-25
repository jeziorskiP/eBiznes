using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class PackingNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackingNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PackingNoteNumber = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    CompletenessDate = table.Column<DateTime>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackingNotes_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackingNoteLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    PackingNoteId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingNoteLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackingNoteLines_PackingNotes_PackingNoteId",
                        column: x => x.PackingNoteId,
                        principalTable: "PackingNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PackingNoteLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackingNoteLines_PackingNoteId",
                table: "PackingNoteLines",
                column: "PackingNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PackingNoteLines_ProductId",
                table: "PackingNoteLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PackingNotes_OrderId",
                table: "PackingNotes",
                column: "OrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackingNoteLines");

            migrationBuilder.DropTable(
                name: "PackingNotes");
        }
    }
}
