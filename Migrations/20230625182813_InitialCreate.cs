using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aktor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", nullable: false),
                    Nazwisko = table.Column<string>(type: "TEXT", nullable: false),
                    DataUrodzenia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    KrajPochodzenia = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aktor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", nullable: false),
                    Nazwisko = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Haslo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tytul = table.Column<string>(type: "TEXT", nullable: false),
                    RokProdukcji = table.Column<int>(type: "INTEGER", nullable: false),
                    Gatunek = table.Column<string>(type: "TEXT", nullable: false),
                    Rezyser = table.Column<string>(type: "TEXT", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: false),
                    // Oceny = table.Column<int>(type: "INTEGER", nullable: false),
                    AktorId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Film_Aktor_AktorId",
                        column: x => x.AktorId,
                        principalTable: "Aktor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ocena",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilmId = table.Column<int>(type: "INTEGER", nullable: true),
                    UzytkownikId = table.Column<int>(type: "INTEGER", nullable: true),
                    OcenaWartosc = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocena", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ocena_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ocena_Uzytkownik_UzytkownikId",
                        column: x => x.UzytkownikId,
                        principalTable: "Uzytkownik",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Film_AktorId",
                table: "Film",
                column: "AktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocena_FilmId",
                table: "Ocena",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocena_UzytkownikId",
                table: "Ocena",
                column: "UzytkownikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ocena");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "Uzytkownik");

            migrationBuilder.DropTable(
                name: "Aktor");
        }
    }
}
