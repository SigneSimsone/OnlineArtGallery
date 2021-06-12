using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineArtGallery.Web.Data.Migrations
{
    public partial class UpdateExhibitionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exhibitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArtistModelExhibitionModel",
                columns: table => new
                {
                    ArtistsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistModelExhibitionModel", x => new { x.ArtistsId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_ArtistModelExhibitionModel_Artists_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistModelExhibitionModel_Exhibitions_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Exhibitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtworkModelExhibitionModel",
                columns: table => new
                {
                    ArtworksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExhibitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtworkModelExhibitionModel", x => new { x.ArtworksId, x.ExhibitsId });
                    table.ForeignKey(
                        name: "FK_ArtworkModelExhibitionModel_Artworks_ArtworksId",
                        column: x => x.ArtworksId,
                        principalTable: "Artworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtworkModelExhibitionModel_Exhibitions_ExhibitsId",
                        column: x => x.ExhibitsId,
                        principalTable: "Exhibitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistModelExhibitionModel_ParticipantsId",
                table: "ArtistModelExhibitionModel",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkModelExhibitionModel_ExhibitsId",
                table: "ArtworkModelExhibitionModel",
                column: "ExhibitsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistModelExhibitionModel");

            migrationBuilder.DropTable(
                name: "ArtworkModelExhibitionModel");

            migrationBuilder.DropTable(
                name: "Exhibitions");
        }
    }
}
