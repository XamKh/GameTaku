using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class ProductProductGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductGenre_ProductGenres_ProductGenreName",
                table: "ProductProductGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductGenre_Products_ProductID",
                table: "ProductProductGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductGenre",
                table: "ProductProductGenre");

            migrationBuilder.RenameTable(
                name: "ProductProductGenre",
                newName: "ProductProductGenres");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProductGenre_ProductID",
                table: "ProductProductGenres",
                newName: "IX_ProductProductGenres_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProductGenre_ProductGenreName",
                table: "ProductProductGenres",
                newName: "IX_ProductProductGenres_ProductGenreName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProductGenres",
                table: "ProductProductGenres",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductGenres_ProductGenres_ProductGenreName",
                table: "ProductProductGenres",
                column: "ProductGenreName",
                principalTable: "ProductGenres",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductGenres_Products_ProductID",
                table: "ProductProductGenres",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductGenres_ProductGenres_ProductGenreName",
                table: "ProductProductGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductGenres_Products_ProductID",
                table: "ProductProductGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductProductGenres",
                table: "ProductProductGenres");

            migrationBuilder.RenameTable(
                name: "ProductProductGenres",
                newName: "ProductProductGenre");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProductGenres_ProductID",
                table: "ProductProductGenre",
                newName: "IX_ProductProductGenre_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProductGenres_ProductGenreName",
                table: "ProductProductGenre",
                newName: "IX_ProductProductGenre_ProductGenreName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductProductGenre",
                table: "ProductProductGenre",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductGenre_ProductGenres_ProductGenreName",
                table: "ProductProductGenre",
                column: "ProductGenreName",
                principalTable: "ProductGenres",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductGenre_Products_ProductID",
                table: "ProductProductGenre",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
