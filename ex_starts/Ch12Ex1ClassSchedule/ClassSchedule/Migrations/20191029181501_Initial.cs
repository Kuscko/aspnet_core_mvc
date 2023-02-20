using Microsoft.EntityFrameworkCore.Migrations;

namespace Ch12Ex1ClassSchedule.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    DayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayId);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Number = table.Column<int>(nullable: false),
                    MilitaryTime = table.Column<string>(maxLength: 4, nullable: false),
                    TeacherId = table.Column<int>(nullable: false),
                    DayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_Classes_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "DayId", "Name" },
                values: new object[,]
                {
                    { 1, "Monday" },
                    { 2, "Tuesday" },
                    { 3, "Wednesday" },
                    { 4, "Thursday" },
                    { 5, "Friday" },
                    { 6, "Saturday" },
                    { 7, "Sunday" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "TeacherId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Anne", "Sullivan" },
                    { 2, "Maria", "Montessori" },
                    { 3, "Martin Luther", "King" },
                    { 4, "", "Aristotle" },
                    { 5, "Jaime", "Escalante" }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "DayId", "MilitaryTime", "Number", "TeacherId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "1100", 101, 1, "Sign Language" },
                    { 2, 2, "1100", 301, 1, "Sign Language" },
                    { 5, 3, "1000", 101, 2, "Early Childhood Education" },
                    { 6, 3, "1000", 401, 2, "Early Childhood Education" },
                    { 9, 4, "1400", 101, 3, "Nonviolence and Social Change" },
                    { 10, 5, "1400", 201, 3, "Nonviolence and Social Change" },
                    { 3, 4, "1300", 101, 4, "Logic" },
                    { 4, 4, "1500", 201, 4, "Logic" },
                    { 7, 1, "1300", 101, 5, "Calculus" },
                    { 8, 3, "1300", 201, 5, "Calculus" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_DayId",
                table: "Classes",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
