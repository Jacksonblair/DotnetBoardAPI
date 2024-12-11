using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class updateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AreaId",
                table: "Items",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Areas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Items_AreaId",
                table: "Items",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_ProjectId",
                table: "Areas",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Projects_ProjectId",
                table: "Areas",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Areas_AreaId",
                table: "Items",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Projects_ProjectId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Areas_AreaId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_AreaId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Areas_ProjectId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Areas");
        }
    }
}
