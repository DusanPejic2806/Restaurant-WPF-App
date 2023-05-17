using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoranProjekat.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artikli",
                columns: table => new
                {
                    SifraArtikla = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivArtikla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CenaArtikla = table.Column<double>(type: "float", nullable: false),
                    DostupnaKolicina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikli", x => x.SifraArtikla);
                });

            migrationBuilder.CreateTable(
                name: "Stolovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojStola = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stolovi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Zaposleni",
                columns: table => new
                {
                    ZaposleniId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposleni", x => x.ZaposleniId);
                });

            migrationBuilder.CreateTable(
                name: "Racuni",
                columns: table => new
                {
                    BrojRacuna = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datumIzdavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdZaposlenogZaposleniId = table.Column<int>(type: "int", nullable: false),
                    IdStolaBrojStola = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racuni", x => x.BrojRacuna);
                    table.ForeignKey(
                        name: "FK_Racuni_Stolovi_IdStolaBrojStola",
                        column: x => x.IdStolaBrojStola,
                        principalTable: "Stolovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Racuni_Zaposleni_IdZaposlenogZaposleniId",
                        column: x => x.IdZaposlenogZaposleniId,
                        principalTable: "Zaposleni",
                        principalColumn: "ZaposleniId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtikliRacuni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtikliNaRacunuSifraArtikla = table.Column<int>(type: "int", nullable: false),
                    RacuniSaArtiklomBrojRacuna = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtikliRacuni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtikliRacuni_Artikli_ArtikliNaRacunuSifraArtikla",
                        column: x => x.ArtikliNaRacunuSifraArtikla,
                        principalTable: "Artikli",
                        principalColumn: "SifraArtikla",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtikliRacuni_Racuni_RacuniSaArtiklomBrojRacuna",
                        column: x => x.RacuniSaArtiklomBrojRacuna,
                        principalTable: "Racuni",
                        principalColumn: "BrojRacuna",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtikliRacuni_ArtikliNaRacunuSifraArtikla",
                table: "ArtikliRacuni",
                column: "ArtikliNaRacunuSifraArtikla");

            migrationBuilder.CreateIndex(
                name: "IX_ArtikliRacuni_RacuniSaArtiklomBrojRacuna",
                table: "ArtikliRacuni",
                column: "RacuniSaArtiklomBrojRacuna");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_IdStolaBrojStola",
                table: "Racuni",
                column: "IdStolaBrojStola");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_IdZaposlenogZaposleniId",
                table: "Racuni",
                column: "IdZaposlenogZaposleniId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtikliRacuni");

            migrationBuilder.DropTable(
                name: "Artikli");

            migrationBuilder.DropTable(
                name: "Racuni");

            migrationBuilder.DropTable(
                name: "Stolovi");

            migrationBuilder.DropTable(
                name: "Zaposleni");
        }
    }
}
