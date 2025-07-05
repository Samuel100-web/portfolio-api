using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamuelPortfolioAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingAndLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectReactions_Projects_ProjectId",
                table: "ProjectReactions");

            migrationBuilder.DropIndex(
                name: "IX_ProjectReactions_ProjectId",
                table: "ProjectReactions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProjectReactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectReactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ProjectReactions_ProjectId",
                table: "ProjectReactions",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectReactions_Projects_ProjectId",
                table: "ProjectReactions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
