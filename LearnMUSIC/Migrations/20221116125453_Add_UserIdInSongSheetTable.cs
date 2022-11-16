using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMUSIC.Migrations
{
    public partial class Add_UserIdInSongSheetTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserModuleAccess_Module_ModuleId",
                table: "UserModuleAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_UserModuleAccess_Users_UserId",
                table: "UserModuleAccess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserModuleAccess",
                table: "UserModuleAccess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Module",
                table: "Module");

            migrationBuilder.RenameTable(
                name: "UserModuleAccess",
                newName: "UserModuleAccesses");

            migrationBuilder.RenameTable(
                name: "Module",
                newName: "Modules");

            migrationBuilder.RenameIndex(
                name: "IX_UserModuleAccess_UserId",
                table: "UserModuleAccesses",
                newName: "IX_UserModuleAccesses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserModuleAccess_ModuleId",
                table: "UserModuleAccesses",
                newName: "IX_UserModuleAccesses_ModuleId");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "SongSheets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserModuleAccesses",
                table: "UserModuleAccesses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modules",
                table: "Modules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserModuleAccesses_Modules_ModuleId",
                table: "UserModuleAccesses",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserModuleAccesses_Users_UserId",
                table: "UserModuleAccesses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserModuleAccesses_Modules_ModuleId",
                table: "UserModuleAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserModuleAccesses_Users_UserId",
                table: "UserModuleAccesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserModuleAccesses",
                table: "UserModuleAccesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modules",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SongSheets");

            migrationBuilder.RenameTable(
                name: "UserModuleAccesses",
                newName: "UserModuleAccess");

            migrationBuilder.RenameTable(
                name: "Modules",
                newName: "Module");

            migrationBuilder.RenameIndex(
                name: "IX_UserModuleAccesses_UserId",
                table: "UserModuleAccess",
                newName: "IX_UserModuleAccess_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserModuleAccesses_ModuleId",
                table: "UserModuleAccess",
                newName: "IX_UserModuleAccess_ModuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserModuleAccess",
                table: "UserModuleAccess",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Module",
                table: "Module",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserModuleAccess_Module_ModuleId",
                table: "UserModuleAccess",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserModuleAccess_Users_UserId",
                table: "UserModuleAccess",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
