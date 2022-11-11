using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class AlterPegawaiBidang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_pegawai_BidangID",
                table: "pegawai",
                column: "BidangID");

            migrationBuilder.AddForeignKey(
                name: "FK_pegawai_bidang_BidangID",
                table: "pegawai",
                column: "BidangID",
                principalTable: "bidang",
                principalColumn: "BidangID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pegawai_bidang_BidangID",
                table: "pegawai");

            migrationBuilder.DropIndex(
                name: "IX_pegawai_BidangID",
                table: "pegawai");
        }
    }
}
