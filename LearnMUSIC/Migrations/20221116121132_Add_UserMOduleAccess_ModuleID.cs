using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMUSIC.Migrations
{
    public partial class Add_UserMOduleAccess_ModuleID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserModuleAccess",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ModuleId = table.Column<long>(type: "bigint", nullable: false),
                    HasAccess = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModuleAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserModuleAccess_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserModuleAccess_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserModuleAccess_ModuleId",
                table: "UserModuleAccess",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModuleAccess_UserId",
                table: "UserModuleAccess",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserModuleAccess");

            migrationBuilder.DropTable(
                name: "Module");
        }
    }
}
