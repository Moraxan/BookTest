using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTest.Data.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "J.K. Rowling" },
                    { 2, "J.R.R. Tolkien" },
                    { 3, "George R.R. Martin" },
                    { 4, "Stephen King" },
                    { 5, "J.D. Salinger" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "PublicationDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Philosopher's Stone" },
                    { 2, new DateTime(1998, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Chamber of Secrets" },
                    { 3, new DateTime(1954, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings: The Fellowship of the Ring" },
                    { 4, new DateTime(1954, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings: The Two Towers" },
                    { 5, new DateTime(1996, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Game of Thrones" },
                    { 6, new DateTime(1998, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Clash of Kings" },
                    { 7, new DateTime(1977, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Shining" },
                    { 8, new DateTime(1978, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Stand" },
                    { 9, new DateTime(1951, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Catcher in the Rye" },
                    { 10, new DateTime(1953, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nine Stories" }
                });

            migrationBuilder.InsertData(
                table: "AuthorBooks",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 4, 7 },
                    { 4, 8 },
                    { 5, 9 },
                    { 5, 10 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 9 });

            migrationBuilder.DeleteData(
                table: "AuthorBooks",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 10 });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
