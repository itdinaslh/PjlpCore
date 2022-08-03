using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class DivisionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "divisi",
                columns: table => new
                {
                    DivisiID = table.Column<Guid>(type: "uuid", nullable: false),
                    NamaDivisi = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BidangID = table.Column<Guid>(type: "uuid", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_divisi", x => x.DivisiID);
                    table.ForeignKey(
                        name: "FK_divisi_bidang_BidangID",
                        column: x => x.BidangID,
                        principalTable: "bidang",
                        principalColumn: "BidangID");
                });

            migrationBuilder.CreateTable(
                name: "jabatan",
                columns: table => new
                {
                    JabatanID = table.Column<Guid>(type: "uuid", nullable: false),
                    NamaJabatan = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BidangID = table.Column<Guid>(type: "uuid", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jabatan", x => x.JabatanID);
                    table.ForeignKey(
                        name: "FK_jabatan_bidang_BidangID",
                        column: x => x.BidangID,
                        principalTable: "bidang",
                        principalColumn: "BidangID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_divisi_BidangID",
                table: "divisi",
                column: "BidangID");

            migrationBuilder.CreateIndex(
                name: "IX_jabatan_BidangID",
                table: "jabatan",
                column: "BidangID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "divisi");

            migrationBuilder.DropTable(
                name: "jabatan");
        }
    }
}
