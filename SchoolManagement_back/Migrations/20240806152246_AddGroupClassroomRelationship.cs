using Microsoft.EntityFrameworkCore.Migrations;
using SchoolManagement.Infrastructure;

#nullable disable

namespace school_calendar_back.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupClassroomRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Classrooms_ClassroomId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Classrooms_ClassroomId",
                table: "Groups",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Classrooms_ClassroomId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Classrooms_ClassroomId",
                table: "Groups",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
