using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class AlterFilePegawais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersyaratanID",
                table: "FilePegawais",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FilePegawais_PersyaratanID",
                table: "FilePegawais",
                column: "PersyaratanID");

            migrationBuilder.AddForeignKey(
                name: "FK_FilePegawais_persyaratan_PersyaratanID",
                table: "FilePegawais",
                column: "PersyaratanID",
                principalTable: "persyaratan",
                principalColumn: "PersyaratanID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilePegawais_persyaratan_PersyaratanID",
                table: "FilePegawais");

            migrationBuilder.DropIndex(
                name: "IX_FilePegawais_PersyaratanID",
                table: "FilePegawais");

            migrationBuilder.DropColumn(
                name: "PersyaratanID",
                table: "FilePegawais");
        }
    }
}
