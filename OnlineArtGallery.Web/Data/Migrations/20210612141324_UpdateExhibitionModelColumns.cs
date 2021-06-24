using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineArtGallery.Web.Data.Migrations
{
    public partial class UpdateExhibitionModelColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start_date",
                table: "Exhibitions",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End_date",
                table: "Exhibitions",
                newName: "EndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Exhibitions",
                newName: "Start_date");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Exhibitions",
                newName: "End_date");
        }
    }
}
