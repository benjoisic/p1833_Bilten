using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bilten.Data.Migrations
{
    public partial class initialDb : Migration
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

            migrationBuilder.CreateTable(
                name: "Dogadjaj",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrganizacionaJedinicaId = table.Column<int>(nullable: false),
                    PodorganizacionaJedinicaId = table.Column<int>(nullable: false),
                    VrsteId = table.Column<int>(nullable: false),
                    KategorijeId = table.Column<int>(nullable: false),
                    DatumDogadjaja = table.Column<DateTime>(nullable: true),
                    MjestoDogadjaja = table.Column<string>(nullable: true),
                    DatumPrijave = table.Column<DateTime>(nullable: true),
                    Prijavitelj = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true),
                    Odabran = table.Column<bool>(nullable: true),
                    SlikaPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogadjaj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dogadjaj_Kategorije_KategorijeId",
                        column: x => x.KategorijeId,
                        principalTable: "Kategorije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dogadjaj_OrganizacionaJedinica_OrganizacionaJedinicaId",
                        column: x => x.OrganizacionaJedinicaId,
                        principalTable: "OrganizacionaJedinica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Dogadjaj_PodorganizacionaJedinica_PodorganizacionaJedinicaId",
                        column: x => x.PodorganizacionaJedinicaId,
                        principalTable: "PodorganizacionaJedinica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dogadjaj_Vrste_VrsteId",
                        column: x => x.VrsteId,
                        principalTable: "Vrste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DogadjajiMjere",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DogadjajId = table.Column<int>(nullable: false),
                    MjereId = table.Column<int>(nullable: false),
                    MjeraPoduzeta = table.Column<bool>(nullable: false)
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
                name: "IX_Dogadjaj_KategorijeId",
                table: "Dogadjaj",
                column: "KategorijeId");

            migrationBuilder.CreateIndex(
                name: "IX_Dogadjaj_OrganizacionaJedinicaId",
                table: "Dogadjaj",
                column: "OrganizacionaJedinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dogadjaj_PodorganizacionaJedinicaId",
                table: "Dogadjaj",
                column: "PodorganizacionaJedinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dogadjaj_VrsteId",
                table: "Dogadjaj",
                column: "VrsteId");

            migrationBuilder.CreateIndex(
                name: "IX_DogadjajiMjere_DogadjajId",
                table: "DogadjajiMjere",
                column: "DogadjajId");

            migrationBuilder.CreateIndex(
                name: "IX_DogadjajiMjere_MjereId",
                table: "DogadjajiMjere",
                column: "MjereId");

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
                name: "IX_PodorganizacionaJedinica_OrganizacionaJedinicaId",
                table: "PodorganizacionaJedinica",
                column: "OrganizacionaJedinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vrste_KategorijeId",
                table: "Vrste",
                column: "KategorijeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DogadjajiMjere");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Dogadjaj");

            migrationBuilder.DropTable(
                name: "Mjere");

            migrationBuilder.DropTable(
                name: "KorisnickiNalog");

            migrationBuilder.DropTable(
                name: "VrstaKorisnika");

            migrationBuilder.DropTable(
                name: "PodorganizacionaJedinica");

            migrationBuilder.DropTable(
                name: "Vrste");

            migrationBuilder.DropTable(
                name: "OrganizacionaJedinica");

            migrationBuilder.DropTable(
                name: "Kategorije");
        }
    }
}
