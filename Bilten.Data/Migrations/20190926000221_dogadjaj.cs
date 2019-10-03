using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bilten.Data.Migrations
{
    public partial class dogadjaj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Opis = table.Column<string>(nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dogadjaj_PodorganizacionaJedinica_PodorganizacionaJedinicaId",
                        column: x => x.PodorganizacionaJedinicaId,
                        principalTable: "PodorganizacionaJedinica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Dogadjaj_Vrste_VrsteId",
                        column: x => x.VrsteId,
                        principalTable: "Vrste",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dogadjaj");
        }
    }
}
