using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineArtGallery.Web.Data.Migrations
{
    public partial class RenameTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtworkModel_Artists_ArtistId",
                table: "ArtworkModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtworkModel_StyleModel_StyleNameofStyle",
                table: "ArtworkModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtworkModelUserModel_ArtworkModel_ArtworksId",
                table: "ArtworkModelUserModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StyleModel",
                table: "StyleModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtworkModel",
                table: "ArtworkModel");

            migrationBuilder.RenameTable(
                name: "StyleModel",
                newName: "Styles");

            migrationBuilder.RenameTable(
                name: "ArtworkModel",
                newName: "Artworks");

            migrationBuilder.RenameIndex(
                name: "IX_ArtworkModel_StyleNameofStyle",
                table: "Artworks",
                newName: "IX_Artworks_StyleNameofStyle");

            migrationBuilder.RenameIndex(
                name: "IX_ArtworkModel_ArtistId",
                table: "Artworks",
                newName: "IX_Artworks_ArtistId");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Artworks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Artworks",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Styles",
                table: "Styles",
                column: "NameofStyle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Artworks",
                table: "Artworks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtworkModelUserModel_Artworks_ArtworksId",
                table: "ArtworkModelUserModel",
                column: "ArtworksId",
                principalTable: "Artworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Artists_ArtistId",
                table: "Artworks",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Styles_StyleNameofStyle",
                table: "Artworks",
                column: "StyleNameofStyle",
                principalTable: "Styles",
                principalColumn: "NameofStyle",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtworkModelUserModel_Artworks_ArtworksId",
                table: "ArtworkModelUserModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Artists_ArtistId",
                table: "Artworks");

            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Styles_StyleNameofStyle",
                table: "Artworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Styles",
                table: "Styles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Artworks",
                table: "Artworks");

            migrationBuilder.RenameTable(
                name: "Styles",
                newName: "StyleModel");

            migrationBuilder.RenameTable(
                name: "Artworks",
                newName: "ArtworkModel");

            migrationBuilder.RenameIndex(
                name: "IX_Artworks_StyleNameofStyle",
                table: "ArtworkModel",
                newName: "IX_ArtworkModel_StyleNameofStyle");

            migrationBuilder.RenameIndex(
                name: "IX_Artworks_ArtistId",
                table: "ArtworkModel",
                newName: "IX_ArtworkModel_ArtistId");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "ArtworkModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "ArtworkModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StyleModel",
                table: "StyleModel",
                column: "NameofStyle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtworkModel",
                table: "ArtworkModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtworkModel_Artists_ArtistId",
                table: "ArtworkModel",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtworkModel_StyleModel_StyleNameofStyle",
                table: "ArtworkModel",
                column: "StyleNameofStyle",
                principalTable: "StyleModel",
                principalColumn: "NameofStyle",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtworkModelUserModel_ArtworkModel_ArtworksId",
                table: "ArtworkModelUserModel",
                column: "ArtworksId",
                principalTable: "ArtworkModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
