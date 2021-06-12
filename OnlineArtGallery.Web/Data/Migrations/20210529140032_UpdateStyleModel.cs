using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineArtGallery.Web.Data.Migrations
{
    public partial class UpdateStyleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Styles_StyleNameofStyle",
                table: "Artworks");

            migrationBuilder.RenameColumn(
                name: "NameofStyle",
                table: "Styles",
                newName: "Style");

            migrationBuilder.RenameColumn(
                name: "StyleNameofStyle",
                table: "Artworks",
                newName: "Style1");

            migrationBuilder.RenameIndex(
                name: "IX_Artworks_StyleNameofStyle",
                table: "Artworks",
                newName: "IX_Artworks_Style1");

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Styles_Style1",
                table: "Artworks",
                column: "Style1",
                principalTable: "Styles",
                principalColumn: "Style",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Styles_Style1",
                table: "Artworks");

            migrationBuilder.RenameColumn(
                name: "Style",
                table: "Styles",
                newName: "NameofStyle");

            migrationBuilder.RenameColumn(
                name: "Style1",
                table: "Artworks",
                newName: "StyleNameofStyle");

            migrationBuilder.RenameIndex(
                name: "IX_Artworks_Style1",
                table: "Artworks",
                newName: "IX_Artworks_StyleNameofStyle");

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Styles_StyleNameofStyle",
                table: "Artworks",
                column: "StyleNameofStyle",
                principalTable: "Styles",
                principalColumn: "NameofStyle",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
