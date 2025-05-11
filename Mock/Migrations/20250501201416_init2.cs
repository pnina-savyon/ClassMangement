using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mock.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_Students_StudentId",
                table: "Chairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyAttendances_Students_StudentId",
                table: "DailyAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_Students_StudentId",
                table: "Marks");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "User",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "User",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AttentionLevel",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChairId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusSocial",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "User",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SurveyAnswerId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_ChairId",
                table: "User",
                column: "ChairId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ClassId",
                table: "User",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_User_StudentId",
                table: "User",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SurveyAnswerId",
                table: "User",
                column: "SurveyAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_User_StudentId",
                table: "Chairs",
                column: "StudentId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_User_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyAttendances_User_StudentId",
                table: "DailyAttendances",
                column: "StudentId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_User_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Chairs_ChairId",
                table: "User",
                column: "ChairId",
                principalTable: "Chairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Classes_ClassId",
                table: "User",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_SurveyAnswers_SurveyAnswerId",
                table: "User",
                column: "SurveyAnswerId",
                principalTable: "SurveyAnswers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_StudentId",
                table: "User",
                column: "StudentId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_User_StudentId",
                table: "Chairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_User_TeacherId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyAttendances_User_StudentId",
                table: "DailyAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_User_StudentId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Chairs_ChairId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Classes_ClassId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_SurveyAnswers_SurveyAnswerId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_User_StudentId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ChairId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ClassId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_StudentId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_SurveyAnswerId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AttentionLevel",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ChairId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StatusSocial",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SurveyAnswerId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Teachers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Teachers",
                newName: "TeacherId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "TeacherId");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChairId = table.Column<int>(type: "int", nullable: true),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttentionLevel = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    StatusSocial = table.Column<int>(type: "int", nullable: false),
                    StudentId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SurveyAnswerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Chairs_ChairId",
                        column: x => x.ChairId,
                        principalTable: "Chairs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Students_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Students_StudentId1",
                        column: x => x.StudentId1,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                    table.ForeignKey(
                        name: "FK_Students_SurveyAnswers_SurveyAnswerId",
                        column: x => x.SurveyAnswerId,
                        principalTable: "SurveyAnswers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ChairId",
                table: "Students",
                column: "ChairId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId",
                table: "Students",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentId1",
                table: "Students",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SurveyAnswerId",
                table: "Students",
                column: "SurveyAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_Students_StudentId",
                table: "Chairs",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyAttendances_Students_StudentId",
                table: "DailyAttendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_Students_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
