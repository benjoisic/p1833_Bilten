using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bilten.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KorisnickiNalog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Lozinka = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VrstaKorisnika",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrstaKorisnika", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mjere",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Opis = table.Column<string>(nullable: true),
                    KategorijeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mjere", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mjere_Kategorije_KategorijeId",
                        column: x => x.KategorijeId,
                        principalTable: "Kategorije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vrste",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    KategorijeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vrste", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vrste_Kategorije_KategorijeId",
                        column: x => x.KategorijeId,
                        principalTable: "Kategorije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImePrezime = table.Column<string>(nullable: true),
                    JMBG = table.Column<int>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    VrstaKorisnikaId = table.Column<int>(nullable: false),
                    KorisnickiNalogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Korisnici_KorisnickiNalog_KorisnickiNalogId",
                        column: x => x.KorisnickiNalogId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Korisnici_VrstaKorisnika_VrstaKorisnikaId",
                        column: x => x.VrstaKorisnikaId,
                        principalTable: "VrstaKorisnika",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_KorisnickiNalogId",
                table: "Korisnici",
                column: "KorisnickiNalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_VrstaKorisnikaId",
                table: "Korisnici",
                column: "VrstaKorisnikaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mjere_KategorijeId",
                table: "Mjere",
                column: "KategorijeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vrste_KategorijeId",
                table: "Vrste",
                column: "KategorijeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Mjere");

            migrationBuilder.DropTable(
                name: "Vrste");

            migrationBuilder.DropTable(
                name: "KorisnickiNalog");

            migrationBuilder.DropTable(
                name: "VrstaKorisnika");

            migrationBuilder.DropTable(
                name: "Kategorije");
        }
    }
}
