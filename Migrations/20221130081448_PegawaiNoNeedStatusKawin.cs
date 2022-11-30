using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class PegawaiNoNeedStatusKawin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pegawai_statuskawin_StatusKawinID",
                table: "pegawai");

            migrationBuilder.DropTable(
                name: "statuskawin");

            migrationBuilder.DropIndex(
                name: "IX_pegawai_StatusKawinID",
                table: "pegawai");

            migrationBuilder.DropColumn(
                name: "StatusKawinID",
                table: "pegawai");

            migrationBuilder.AddColumn<string>(
                name: "Tanggungan",
                table: "pegawai",
                type: "varchar(3)",
                maxLength: 3,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tanggungan",
                table: "pegawai");

            migrationBuilder.AddColumn<int>(
                name: "StatusKawinID",
                table: "pegawai",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "statuskawin",
                columns: table => new
                {
                    StatusKawinID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    NamaStatus = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuskawin", x => x.StatusKawinID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_pegawai_StatusKawinID",
                table: "pegawai",
                column: "StatusKawinID");

            migrationBuilder.AddForeignKey(
                name: "FK_pegawai_statuskawin_StatusKawinID",
                table: "pegawai",
                column: "StatusKawinID",
                principalTable: "statuskawin",
                principalColumn: "StatusKawinID");
        }
    }
}
