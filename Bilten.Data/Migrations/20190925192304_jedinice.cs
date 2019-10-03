using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bilten.Data.Migrations
{
    public partial class jedinice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizacionaJedinica",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizacionaJedinica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PodorganizacionaJedinica",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    OrganizacionaJedinicaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PodorganizacionaJedinica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PodorganizacionaJedinica_OrganizacionaJedinica_OrganizacionaJedinicaId",
                        column: x => x.OrganizacionaJedinicaId,
                        principalTable: "OrganizacionaJedinica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PodorganizacionaJedinica_OrganizacionaJedinicaId",
                table: "PodorganizacionaJedinica",
                column: "OrganizacionaJedinicaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PodorganizacionaJedinica");

            migrationBuilder.DropTable(
                name: "OrganizacionaJedinica");
        }
    }
}
