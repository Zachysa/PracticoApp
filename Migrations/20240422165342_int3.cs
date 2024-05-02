using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practico.Migrations
{
    /// <inheritdoc />
    public partial class int3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "tbQuestion",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Default",
                table: "tbQuestion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tbSurvey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdEmployee = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbSurvey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbSurveyAnswerQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSurvey = table.Column<int>(type: "int", nullable: false),
                    IdQuestion = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbSurveyAnswerQuestion", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbSurvey");

            migrationBuilder.DropTable(
                name: "tbSurveyAnswerQuestion");

            migrationBuilder.DropColumn(
                name: "Default",
                table: "tbQuestion");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tbQuestion",
                newName: "id");
        }
    }
}
