using Microsoft.EntityFrameworkCore.Migrations;

namespace C_Sharp_WeddingPlanner.Migrations
{
    public partial class _3Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Association_Users_UserId",
                table: "Association");

            migrationBuilder.DropForeignKey(
                name: "FK_Association_Wedding_WeddingId",
                table: "Association");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Association",
                table: "Association");

            migrationBuilder.RenameTable(
                name: "Association",
                newName: "Associations");

            migrationBuilder.RenameIndex(
                name: "IX_Association_WeddingId",
                table: "Associations",
                newName: "IX_Associations_WeddingId");

            migrationBuilder.RenameIndex(
                name: "IX_Association_UserId",
                table: "Associations",
                newName: "IX_Associations_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Associations",
                table: "Associations",
                column: "AsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_Users_UserId",
                table: "Associations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_Wedding_WeddingId",
                table: "Associations",
                column: "WeddingId",
                principalTable: "Wedding",
                principalColumn: "WeddingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associations_Users_UserId",
                table: "Associations");

            migrationBuilder.DropForeignKey(
                name: "FK_Associations_Wedding_WeddingId",
                table: "Associations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Associations",
                table: "Associations");

            migrationBuilder.RenameTable(
                name: "Associations",
                newName: "Association");

            migrationBuilder.RenameIndex(
                name: "IX_Associations_WeddingId",
                table: "Association",
                newName: "IX_Association_WeddingId");

            migrationBuilder.RenameIndex(
                name: "IX_Associations_UserId",
                table: "Association",
                newName: "IX_Association_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Association",
                table: "Association",
                column: "AsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Association_Users_UserId",
                table: "Association",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Association_Wedding_WeddingId",
                table: "Association",
                column: "WeddingId",
                principalTable: "Wedding",
                principalColumn: "WeddingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
