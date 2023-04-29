using Microsoft.EntityFrameworkCore.Migrations;

namespace Deeps.BookStore.Migrations
{
    public partial class addedbookpdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookPdfUrl",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookPdfUrl",
                table: "Books");
        }
    }
}
