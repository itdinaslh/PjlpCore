using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class AddTupoksi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tupoksi",
                columns: table => new
                {
                    TupoksiID = table.Column<Guid>(type: "uuid", nullable: false),
                    NamaTupoksi = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DivisiID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tupoksi", x => x.TupoksiID);
                    table.ForeignKey(
                        name: "FK_tupoksi_divisi_DivisiID",
                        column: x => x.DivisiID,
                        principalTable: "divisi",
                        principalColumn: "DivisiID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tupoksi_DivisiID",
                table: "tupoksi",
                column: "DivisiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tupoksi");
        }
    }
}
