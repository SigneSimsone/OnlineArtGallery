using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineArtGallery.Web.Data.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackModel_Artworks_ArtworkId",
                table: "FeedbackModel");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackModel_AspNetUsers_UserId",
                table: "FeedbackModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedbackModel",
                table: "FeedbackModel");

            migrationBuilder.RenameTable(
                name: "FeedbackModel",
                newName: "Feedbacks");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackModel_UserId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackModel_ArtworkId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_ArtworkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Artworks_ArtworkId",
                table: "Feedbacks",
                column: "ArtworkId",
                principalTable: "Artworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_UserId",
                table: "Feedbacks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Artworks_ArtworkId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_UserId",
                table: "Feedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "FeedbackModel");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_UserId",
                table: "FeedbackModel",
                newName: "IX_FeedbackModel_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_ArtworkId",
                table: "FeedbackModel",
                newName: "IX_FeedbackModel_ArtworkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedbackModel",
                table: "FeedbackModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackModel_Artworks_ArtworkId",
                table: "FeedbackModel",
                column: "ArtworkId",
                principalTable: "Artworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackModel_AspNetUsers_UserId",
                table: "FeedbackModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
