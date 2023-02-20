using Microsoft.EntityFrameworkCore.Migrations;

namespace Ch12Ex1ClassSchedule.Migrations
{
    public partial class ChangeTeacherDeleteBehaviorToRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes");

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 1,
                column: "MilitaryTime",
                value: "1500");

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 6,
                column: "DayId",
                value: 5);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes");

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 1,
                column: "MilitaryTime",
                value: "1100");

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 6,
                column: "DayId",
                value: 3);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Teachers_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
