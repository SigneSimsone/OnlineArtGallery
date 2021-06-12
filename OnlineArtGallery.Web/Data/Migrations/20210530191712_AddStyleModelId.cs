using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineArtGallery.Web.Data.Migrations
{
    public partial class AddStyleModelId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Styles_Style1",
                table: "Artworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Styles",
                table: "Styles");

            migrationBuilder.DropIndex(
                name: "IX_Artworks_Style1",
                table: "Artworks");

            migrationBuilder.DropColumn(
                name: "Style1",
                table: "Artworks");

            migrationBuilder.AlterColumn<string>(
                name: "Style",
                table: "Styles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Styles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StyleId",
                table: "Artworks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Styles",
                table: "Styles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_StyleId",
                table: "Artworks",
                column: "StyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Styles_StyleId",
                table: "Artworks",
                column: "StyleId",
                principalTable: "Styles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artworks_Styles_StyleId",
                table: "Artworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Styles",
                table: "Styles");

            migrationBuilder.DropIndex(
                name: "IX_Artworks_StyleId",
                table: "Artworks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Styles");

            migrationBuilder.DropColumn(
                name: "StyleId",
                table: "Artworks");

            migrationBuilder.AlterColumn<string>(
                name: "Style",
                table: "Styles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Style1",
                table: "Artworks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Styles",
                table: "Styles",
                column: "Style");

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_Style1",
                table: "Artworks",
                column: "Style1");

            migrationBuilder.AddForeignKey(
                name: "FK_Artworks_Styles_Style1",
                table: "Artworks",
                column: "Style1",
                principalTable: "Styles",
                principalColumn: "Style",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
