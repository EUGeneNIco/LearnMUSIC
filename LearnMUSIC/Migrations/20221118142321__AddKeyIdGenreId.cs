using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMUSIC.Migrations
{
    public partial class _AddKeyIdGenreId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeySignature",
                table: "SongSheets");

            migrationBuilder.AddColumn<long>(
                name: "GenreId",
                table: "SongSheets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "KeySignatureId",
                table: "SongSheets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SongSheets_GenreId",
                table: "SongSheets",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_SongSheets_KeySignatureId",
                table: "SongSheets",
                column: "KeySignatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_SongSheets_CodeListValues_GenreId",
                table: "SongSheets",
                column: "GenreId",
                principalTable: "CodeListValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SongSheets_CodeListValues_KeySignatureId",
                table: "SongSheets",
                column: "KeySignatureId",
                principalTable: "CodeListValues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongSheets_CodeListValues_GenreId",
                table: "SongSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_SongSheets_CodeListValues_KeySignatureId",
                table: "SongSheets");

            migrationBuilder.DropIndex(
                name: "IX_SongSheets_GenreId",
                table: "SongSheets");

            migrationBuilder.DropIndex(
                name: "IX_SongSheets_KeySignatureId",
                table: "SongSheets");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "SongSheets");

            migrationBuilder.DropColumn(
                name: "KeySignatureId",
                table: "SongSheets");

            migrationBuilder.AddColumn<string>(
                name: "KeySignature",
                table: "SongSheets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
