using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessObjects.Migrations
{
    public partial class CategoriesToTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTagMaps_PostCategories_TagID",
                table: "PostTagMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostCategories",
                table: "PostCategories");

            migrationBuilder.RenameTable(
                name: "PostCategories",
                newName: "PostTags");

            migrationBuilder.RenameIndex(
                name: "IX_PostCategories_TagName",
                table: "PostTags",
                newName: "IX_PostTags_TagName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags",
                column: "TagID");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTagMaps_PostTags_TagID",
                table: "PostTagMaps",
                column: "TagID",
                principalTable: "PostTags",
                principalColumn: "TagID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTagMaps_PostTags_TagID",
                table: "PostTagMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags");

            migrationBuilder.RenameTable(
                name: "PostTags",
                newName: "PostCategories");

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_TagName",
                table: "PostCategories",
                newName: "IX_PostCategories_TagName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostCategories",
                table: "PostCategories",
                column: "TagID");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTagMaps_PostCategories_TagID",
                table: "PostTagMaps",
                column: "TagID",
                principalTable: "PostCategories",
                principalColumn: "TagID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
