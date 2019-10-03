using Microsoft.EntityFrameworkCore.Migrations;

namespace Bilten.Data.Migrations
{
    public partial class poduzetaMjera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MjeraPoduzeta",
                table: "DogadjajiMjere",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MjeraPoduzeta",
                table: "DogadjajiMjere");
        }
    }
}
