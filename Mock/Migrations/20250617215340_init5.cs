using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChairId",
                table: "Chairs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chairs_ChairId",
                table: "Chairs",
                column: "ChairId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_Chairs_ChairId",
                table: "Chairs",
                column: "ChairId",
                principalTable: "Chairs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_Chairs_ChairId",
                table: "Chairs");

            migrationBuilder.DropIndex(
                name: "IX_Chairs_ChairId",
                table: "Chairs");

            migrationBuilder.DropColumn(
                name: "ChairId",
                table: "Chairs");
        }
    }
}
