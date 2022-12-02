using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class CreateFilePelamar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "filepelamar",
                columns: table => new
                {
                    FilePelamarID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PelamarId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PersyaratanID = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RealName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FilePath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RealPath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileExtension = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filepelamar", x => x.FilePelamarID);
                    table.ForeignKey(
                        name: "FK_filepelamar_pelamar_PelamarId",
                        column: x => x.PelamarId,
                        principalTable: "pelamar",
                        principalColumn: "PelamarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_filepelamar_persyaratan_PersyaratanID",
                        column: x => x.PersyaratanID,
                        principalTable: "persyaratan",
                        principalColumn: "PersyaratanID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_filepelamar_PelamarId",
                table: "filepelamar",
                column: "PelamarId");

            migrationBuilder.CreateIndex(
                name: "IX_filepelamar_PersyaratanID",
                table: "filepelamar",
                column: "PersyaratanID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "filepelamar");
        }
    }
}
