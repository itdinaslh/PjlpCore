using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class AlterContstraintDetailPjlp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DetailPjlps_PegawaiID",
                table: "DetailPjlps");

            migrationBuilder.DropColumn(
                name: "Tanggungan",
                table: "pegawai");

            migrationBuilder.CreateIndex(
                name: "IX_DetailPjlps_PegawaiID",
                table: "DetailPjlps",
                column: "PegawaiID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DetailPjlps_PegawaiID",
                table: "DetailPjlps");

            migrationBuilder.AddColumn<string>(
                name: "Tanggungan",
                table: "pegawai",
                type: "varchar(3)",
                maxLength: 3,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DetailPjlps_PegawaiID",
                table: "DetailPjlps",
                column: "PegawaiID");
        }
    }
}
