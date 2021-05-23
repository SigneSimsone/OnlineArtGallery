using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineArtGallery.Web.Data.Migrations
{
    public partial class CreateArtworkAndStyleModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StyleModel",
                columns: table => new
                {
                    NameofStyle = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StyleModel", x => x.NameofStyle);
                });

            migrationBuilder.CreateTable(
                name: "ArtworkModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Availability = table.Column<bool>(type: "bit", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StyleNameofStyle = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtworkModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtworkModel_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtworkModel_StyleModel_StyleNameofStyle",
                        column: x => x.StyleNameofStyle,
                        principalTable: "StyleModel",
                        principalColumn: "NameofStyle",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtworkModelUserModel",
                columns: table => new
                {
                    ArtworksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtworkModelUserModel", x => new { x.ArtworksId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ArtworkModelUserModel_ArtworkModel_ArtworksId",
                        column: x => x.ArtworksId,
                        principalTable: "ArtworkModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtworkModelUserModel_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkModel_ArtistId",
                table: "ArtworkModel",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkModel_StyleNameofStyle",
                table: "ArtworkModel",
                column: "StyleNameofStyle");

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkModelUserModel_UsersId",
                table: "ArtworkModelUserModel",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtworkModelUserModel");

            migrationBuilder.DropTable(
                name: "ArtworkModel");

            migrationBuilder.DropTable(
                name: "StyleModel");
        }
    }
}
