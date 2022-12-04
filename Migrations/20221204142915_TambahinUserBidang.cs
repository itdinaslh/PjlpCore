using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class TambahinUserBidang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidangUser");

            migrationBuilder.CreateTable(
                name: "userbidang",
                columns: table => new
                {
                    UserBidangID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BidangID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userbidang", x => x.UserBidangID);
                    table.ForeignKey(
                        name: "FK_userbidang_bidang_BidangID",
                        column: x => x.BidangID,
                        principalTable: "bidang",
                        principalColumn: "BidangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userbidang_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_userbidang_BidangID",
                table: "userbidang",
                column: "BidangID");

            migrationBuilder.CreateIndex(
                name: "IX_userbidang_UserID",
                table: "userbidang",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userbidang");

            migrationBuilder.CreateTable(
                name: "BidangUser",
                columns: table => new
                {
                    BidangsBidangID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsersUserID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidangUser", x => new { x.BidangsBidangID, x.UsersUserID });
                    table.ForeignKey(
                        name: "FK_BidangUser_bidang_BidangsBidangID",
                        column: x => x.BidangsBidangID,
                        principalTable: "bidang",
                        principalColumn: "BidangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidangUser_users_UsersUserID",
                        column: x => x.UsersUserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BidangUser_UsersUserID",
                table: "BidangUser",
                column: "UsersUserID");
        }
    }
}
