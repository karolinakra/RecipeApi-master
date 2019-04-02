using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeApi.Migrations
{
    public partial class pk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeID",
                table: "Categories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_RecipeID",
                table: "Categories",
                column: "RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Recipes_RecipeID",
                table: "Categories",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Recipes_RecipeID",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_RecipeID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "RecipeID",
                table: "Categories");
        }
    }
}
