using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineArtGallery.Web.Data.Migrations
{
    public partial class UpdateExhibitionConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistModelExhibitionModel_Exhibitions_ParticipantsId",
                table: "ArtistModelExhibitionModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtworkModelExhibitionModel_Exhibitions_ExhibitsId",
                table: "ArtworkModelExhibitionModel");

            migrationBuilder.RenameColumn(
                name: "ExhibitsId",
                table: "ArtworkModelExhibitionModel",
                newName: "ExhibitionsId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtworkModelExhibitionModel_ExhibitsId",
                table: "ArtworkModelExhibitionModel",
                newName: "IX_ArtworkModelExhibitionModel_ExhibitionsId");

            migrationBuilder.RenameColumn(
                name: "ParticipantsId",
                table: "ArtistModelExhibitionModel",
                newName: "ExhibitionsId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistModelExhibitionModel_ParticipantsId",
                table: "ArtistModelExhibitionModel",
                newName: "IX_ArtistModelExhibitionModel_ExhibitionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistModelExhibitionModel_Exhibitions_ExhibitionsId",
                table: "ArtistModelExhibitionModel",
                column: "ExhibitionsId",
                principalTable: "Exhibitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtworkModelExhibitionModel_Exhibitions_ExhibitionsId",
                table: "ArtworkModelExhibitionModel",
                column: "ExhibitionsId",
                principalTable: "Exhibitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistModelExhibitionModel_Exhibitions_ExhibitionsId",
                table: "ArtistModelExhibitionModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtworkModelExhibitionModel_Exhibitions_ExhibitionsId",
                table: "ArtworkModelExhibitionModel");

            migrationBuilder.RenameColumn(
                name: "ExhibitionsId",
                table: "ArtworkModelExhibitionModel",
                newName: "ExhibitsId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtworkModelExhibitionModel_ExhibitionsId",
                table: "ArtworkModelExhibitionModel",
                newName: "IX_ArtworkModelExhibitionModel_ExhibitsId");

            migrationBuilder.RenameColumn(
                name: "ExhibitionsId",
                table: "ArtistModelExhibitionModel",
                newName: "ParticipantsId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistModelExhibitionModel_ExhibitionsId",
                table: "ArtistModelExhibitionModel",
                newName: "IX_ArtistModelExhibitionModel_ParticipantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistModelExhibitionModel_Exhibitions_ParticipantsId",
                table: "ArtistModelExhibitionModel",
                column: "ParticipantsId",
                principalTable: "Exhibitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtworkModelExhibitionModel_Exhibitions_ExhibitsId",
                table: "ArtworkModelExhibitionModel",
                column: "ExhibitsId",
                principalTable: "Exhibitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
