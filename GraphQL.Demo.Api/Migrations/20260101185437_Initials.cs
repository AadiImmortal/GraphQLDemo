using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphQL.Demo.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Salary = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    GPA = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Couses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Subjects = table.Column<int>(type: "INTEGER", nullable: false),
                    InstructorId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Couses_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CouseDTOStudentDTO",
                columns: table => new
                {
                    CoursesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StudentsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouseDTOStudentDTO", x => new { x.CoursesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CouseDTOStudentDTO_Couses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Couses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CouseDTOStudentDTO_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CouseDTOStudentDTO_StudentsId",
                table: "CouseDTOStudentDTO",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Couses_InstructorId",
                table: "Couses",
                column: "InstructorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouseDTOStudentDTO");

            migrationBuilder.DropTable(
                name: "Couses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Instructors");
        }
    }
}
