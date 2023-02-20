using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieList.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Name", "Rating", "Year" },
                values: new object[] { 1, "Casablanca", 5, 1943 });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Name", "Rating", "Year" },
                values: new object[] { 2, "Wonder Woman", 3, 2017 });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Name", "Rating", "Year" },
                values: new object[] { 3, "Moonstruck", 4, 1988 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
