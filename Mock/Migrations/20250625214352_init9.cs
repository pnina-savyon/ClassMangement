using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    /// <inheritdoc />
    public partial class init9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_Chairs_ChairId",
                table: "Chairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_User_TeacherId",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "StudentStudent");

            migrationBuilder.DropIndex(
                name: "IX_Chairs_ChairId",
                table: "Chairs");

            migrationBuilder.DropColumn(
                name: "ChairId",
                table: "Chairs");

            migrationBuilder.CreateTable(
                name: "ChairNearbyChairs",
                columns: table => new
                {
                    ChairId = table.Column<int>(type: "int", nullable: false),
                    NearbyChairId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChairNearbyChairs", x => new { x.ChairId, x.NearbyChairId });
                    table.ForeignKey(
                        name: "FK_ChairNearbyChairs_Chairs_ChairId",
                        column: x => x.ChairId,
                        principalTable: "Chairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChairNearbyChairs_Chairs_NearbyChairId",
                        column: x => x.NearbyChairId,
                        principalTable: "Chairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentFavoriteFriends",
                columns: table => new
                {
                    FriendId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentFavoriteFriends", x => new { x.FriendId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentFavoriteFriends_User_FriendId",
                        column: x => x.FriendId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentFavoriteFriends_User_StudentId",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentNonFavoriteFriends",
                columns: table => new
                {
                    NonFriendId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentNonFavoriteFriends", x => new { x.NonFriendId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentNonFavoriteFriends_User_NonFriendId",
                        column: x => x.NonFriendId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentNonFavoriteFriends_User_StudentId",
                        column: x => x.StudentId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChairNearbyChairs_NearbyChairId",
                table: "ChairNearbyChairs",
                column: "NearbyChairId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFavoriteFriends_StudentId",
                table: "StudentFavoriteFriends",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentNonFavoriteFriends_StudentId",
                table: "StudentNonFavoriteFriends",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_User_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_User_TeacherId",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "ChairNearbyChairs");

            migrationBuilder.DropTable(
                name: "StudentFavoriteFriends");

            migrationBuilder.DropTable(
                name: "StudentNonFavoriteFriends");

            migrationBuilder.AddColumn<int>(
                name: "ChairId",
                table: "Chairs",
                type: "int",
                nullable: true);

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
                name: "IX_Chairs_ChairId",
                table: "Chairs",
                column: "ChairId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentStudent_NonFavoriteFriendsId",
                table: "StudentStudent",
                column: "NonFavoriteFriendsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_Chairs_ChairId",
                table: "Chairs",
                column: "ChairId",
                principalTable: "Chairs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_User_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
