using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class CreateEventFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eventfiles",
                columns: table => new
                {
                    EventFileID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    PersyaratanID = table.Column<int>(type: "int", nullable: false),
                    JabatanID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IsNew = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eventfiles", x => x.EventFileID);
                    table.ForeignKey(
                        name: "FK_eventfiles_events_EventID",
                        column: x => x.EventID,
                        principalTable: "events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_eventfiles_jabatan_JabatanID",
                        column: x => x.JabatanID,
                        principalTable: "jabatan",
                        principalColumn: "JabatanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_eventfiles_persyaratan_PersyaratanID",
                        column: x => x.PersyaratanID,
                        principalTable: "persyaratan",
                        principalColumn: "PersyaratanID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_eventfiles_EventID",
                table: "eventfiles",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_eventfiles_JabatanID",
                table: "eventfiles",
                column: "JabatanID");

            migrationBuilder.CreateIndex(
                name: "IX_eventfiles_PersyaratanID",
                table: "eventfiles",
                column: "PersyaratanID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eventfiles");
        }
    }
}
