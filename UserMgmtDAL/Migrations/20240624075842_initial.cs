using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserMgmtDAL.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DOB = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mobile = table.Column<string>(type: "nchar(10)", maxLength: 10, nullable: false),
                    RoleId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Admin" },
                    { (byte)2, "Supervisor" },
                    { (byte)3, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DOB", "Email", "Mobile", "Name", "RoleId" },
                values: new object[,]
                {
                    { (byte)1, new DateTime(1998, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "xadmin@mail.com", "9876787656", "XAdmin", (byte)1 },
                    { (byte)2, new DateTime(1998, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "usuper@mail.com", "9876787353", "USupervisor", (byte)2 },
                    { (byte)3, new DateTime(1998, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "puser@mail.com", "6786787656", "PUser", (byte)3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
