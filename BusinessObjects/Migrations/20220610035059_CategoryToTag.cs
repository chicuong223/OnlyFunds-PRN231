using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessObjects.Migrations
{
    public partial class CategoryToTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostCategoryMaps");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "PostCategories",
                newName: "TagName");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "PostCategories",
                newName: "TagID");

            migrationBuilder.RenameIndex(
                name: "IX_PostCategories_CategoryName",
                table: "PostCategories",
                newName: "IX_PostCategories_TagName");

            migrationBuilder.CreateTable(
                name: "PostTagMaps",
                columns: table => new
                {
                    TagID = table.Column<int>(type: "int", nullable: false),
                    PostID = table.Column<int>(type: "int", nullable: false),
                    PostID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTagMaps", x => new { x.TagID, x.PostID });
                    table.ForeignKey(
                        name: "FK_PostTagMaps_PostCategories_TagID",
                        column: x => x.TagID,
                        principalTable: "PostCategories",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTagMaps_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTagMaps_Posts_PostID1",
                        column: x => x.PostID1,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostTagMaps_PostID",
                table: "PostTagMaps",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostTagMaps_PostID1",
                table: "PostTagMaps",
                column: "PostID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTagMaps");

            migrationBuilder.RenameColumn(
                name: "TagName",
                table: "PostCategories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "TagID",
                table: "PostCategories",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_PostCategories_TagName",
                table: "PostCategories",
                newName: "IX_PostCategories_CategoryName");

            migrationBuilder.CreateTable(
                name: "PostCategoryMaps",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    PostID = table.Column<int>(type: "int", nullable: false),
                    PostID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategoryMaps", x => new { x.CategoryID, x.PostID });
                    table.ForeignKey(
                        name: "FK_PostCategoryMaps_PostCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "PostCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCategoryMaps_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCategoryMaps_Posts_PostID1",
                        column: x => x.PostID1,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostCategoryMaps_PostID",
                table: "PostCategoryMaps",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostCategoryMaps_PostID1",
                table: "PostCategoryMaps",
                column: "PostID1");
        }
    }
}
