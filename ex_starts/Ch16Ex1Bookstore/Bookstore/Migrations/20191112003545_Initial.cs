using Microsoft.EntityFrameworkCore.Migrations;

namespace Ch16Ex1Bookstore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 200, nullable: false),
                    LastName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    GenreId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    AuthorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Michelle", "Alexander" },
                    { 26, "Seth", "Grahame-Smith" },
                    { 25, "JK", "Rowling" },
                    { 23, "Augusten", "Burroughs" },
                    { 22, "Sun", "Tzu" },
                    { 21, "Mary", "Shelley" },
                    { 20, "George", "Orwell" },
                    { 19, "Toni", "Morrison" },
                    { 18, "David", "McCullough" },
                    { 17, "Stieg", "Larsson" },
                    { 16, "Aldous", "Huxley" },
                    { 14, "Dashiel", "Hammett" },
                    { 15, "Frank", "Herbert" },
                    { 12, "Tina", "Fey" },
                    { 2, "Stephen E.", "Ambrose" },
                    { 3, "Margaret", "Atwood" },
                    { 4, "Jane", "Austen" },
                    { 13, "Roxane", "Gay" },
                    { 6, "Emily", "Bronte" },
                    { 5, "James", "Baldwin" },
                    { 8, "Ta-Nehisi", "Coates" },
                    { 9, "Jared", "Diamond" },
                    { 10, "Joan", "Didion" },
                    { 11, "Daphne", "Du Maurier" },
                    { 7, "Agatha", "Christie" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "Name" },
                values: new object[,]
                {
                    { "scifi", "Science Fiction" },
                    { "novel", "Novel" },
                    { "memoir", "Memoir" },
                    { "mystery", "Mystery" },
                    { "history", "History" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "GenreId", "Price", "Title" },
                values: new object[,]
                {
                    { 5, "novel", 10.99, "Beloved" },
                    { 15, "history", 15.5, "Guns, Germs, and Steel" },
                    { 9, "history", 15.0, "D-Day" },
                    { 4, "history", 11.5, "Band of Brothers" },
                    { 1, "history", 18.0, "1776" },
                    { 22, "scifi", 12.5, "The Handmaid's Tale" },
                    { 13, "scifi", 6.5, "Frankenstein" },
                    { 11, "scifi", 8.75, "Dune" },
                    { 8, "scifi", 16.25, "Brave New World" },
                    { 2, "scifi", 5.5, "1984" },
                    { 23, "mystery", 10.99, "The Maltese Falcon" },
                    { 21, "mystery", 8.5, "The Girl with the Dragon Tattoo" },
                    { 19, "mystery", 10.99, "Rebecca" },
                    { 20, "history", 5.75, "The Art of War" },
                    { 17, "mystery", 6.75, "Murder on the Orient Express" },
                    { 27, "memoir", 11.0, "Running With Scissors" },
                    { 25, "memoir", 13.5, "The Year of Magical Thinking" },
                    { 16, "memoir", 14.5, "Hunger" },
                    { 10, "memoir", 12.5, "Down and Out in Paris and London" },
                    { 7, "memoir", 4.25, "Bossypants" },
                    { 6, "memoir", 13.5, "Between the World and Me" },
                    { 29, "novel", 9.75, "Harry Potter and the Sorcerer's Stone" },
                    { 28, "novel", 8.75, "Pride and Prejudice and Zombies" },
                    { 26, "novel", 9.0, "Wuthering Heights" },
                    { 18, "novel", 8.5, "Pride and Prejudice" },
                    { 14, "novel", 10.25, "Go Tell it on the Mountain" },
                    { 12, "novel", 9.0, "Emma" },
                    { 3, "mystery", 4.5, "And Then There Were None" },
                    { 24, "history", 13.75, "The New Jim Crow" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId" },
                values: new object[,]
                {
                    { 5, 19 },
                    { 15, 9 },
                    { 9, 2 },
                    { 4, 2 },
                    { 1, 18 },
                    { 22, 3 },
                    { 13, 21 },
                    { 11, 15 },
                    { 8, 16 },
                    { 2, 20 },
                    { 23, 14 },
                    { 21, 17 },
                    { 19, 11 },
                    { 17, 7 },
                    { 3, 7 },
                    { 27, 23 },
                    { 25, 10 },
                    { 16, 13 },
                    { 10, 20 },
                    { 7, 12 },
                    { 6, 8 },
                    { 29, 25 },
                    { 28, 26 },
                    { 28, 4 },
                    { 26, 6 },
                    { 18, 4 },
                    { 14, 5 },
                    { 12, 4 },
                    { 20, 22 },
                    { 24, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
