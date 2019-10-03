using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bilten.Data.Migrations
{
    public partial class dogadjajiMjere : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DogadjajiMjere",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DogadjajId = table.Column<int>(nullable: false),
                    MjereId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogadjajiMjere", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DogadjajiMjere_Dogadjaj_DogadjajId",
                        column: x => x.DogadjajId,
                        principalTable: "Dogadjaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DogadjajiMjere_Mjere_MjereId",
                        column: x => x.MjereId,
                        principalTable: "Mjere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DogadjajiMjere_DogadjajId",
                table: "DogadjajiMjere",
                column: "DogadjajId");

            migrationBuilder.CreateIndex(
                name: "IX_DogadjajiMjere_MjereId",
                table: "DogadjajiMjere",
                column: "MjereId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DogadjajiMjere");
        }
    }
}
