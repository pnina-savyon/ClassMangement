using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_User_StudentId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_StudentId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Column",
                table: "Chairs");

            migrationBuilder.DropColumn(
                name: "Row",
                table: "Chairs");

            migrationBuilder.AddColumn<int>(
                name: "MoralLevel",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCenteral",
                table: "Chairs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFront",
                table: "Chairs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "StudentStudent",
                columns: table => new
                {
                    FavoriteFriendsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NonFavoriteFriendsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentStudent", x => new { x.FavoriteFriendsId, x.NonFavoriteFriendsId });
                    table.ForeignKey(
                        name: "FK_StudentStudent_User_FavoriteFriendsId",
                        column: x => x.FavoriteFriendsId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentStudent_User_NonFavoriteFriendsId",
                        column: x => x.NonFavoriteFriendsId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentStudent_NonFavoriteFriendsId",
                table: "StudentStudent",
                column: "NonFavoriteFriendsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentStudent");

            migrationBuilder.DropColumn(
                name: "MoralLevel",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsCenteral",
                table: "Chairs");

            migrationBuilder.DropColumn(
                name: "IsFront",
                table: "Chairs");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "User",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Column",
                table: "Chairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "Chairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_StudentId",
                table: "User",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_StudentId",
                table: "User",
                column: "StudentId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
