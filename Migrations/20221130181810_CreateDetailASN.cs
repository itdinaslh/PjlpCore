using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class CreateDetailASN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "detailasn",
                columns: table => new
                {
                    DetailAsnID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PegawaiID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NIP = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NRK = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detailasn", x => x.DetailAsnID);
                    table.ForeignKey(
                        name: "FK_detailasn_pegawai_PegawaiID",
                        column: x => x.PegawaiID,
                        principalTable: "pegawai",
                        principalColumn: "PegawaiID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_detailasn_PegawaiID",
                table: "detailasn",
                column: "PegawaiID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detailasn");
        }
    }
}
