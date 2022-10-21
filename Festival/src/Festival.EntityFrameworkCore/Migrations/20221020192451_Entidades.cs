using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Festival.Migrations
{
    public partial class Entidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppApuestas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tenant = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstaPagada = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppApuestas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppEquipos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tenant = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grupo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Siglas = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEquipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppPartidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tenant = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultadoEquipoA = table.Column<int>(type: "int", nullable: false),
                    ResultadoEquipoB = table.Column<int>(type: "int", nullable: false),
                    FechaPartido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EquipoAId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipoBId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPartidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppPredicciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tenant = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrediccionResultadoEquipoA = table.Column<int>(type: "int", nullable: false),
                    PrediccionResultadoEquipoB = table.Column<int>(type: "int", nullable: false),
                    PuntosObtenidos = table.Column<int>(type: "int", nullable: false),
                    ApuestaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPredicciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPredicciones_AppApuestas_ApuestaId",
                        column: x => x.ApuestaId,
                        principalTable: "AppApuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPredicciones_AppPartidos_PartidoId",
                        column: x => x.PartidoId,
                        principalTable: "AppPartidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPredicciones_ApuestaId",
                table: "AppPredicciones",
                column: "ApuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPredicciones_PartidoId",
                table: "AppPredicciones",
                column: "PartidoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppEquipos");

            migrationBuilder.DropTable(
                name: "AppPredicciones");

            migrationBuilder.DropTable(
                name: "AppApuestas");

            migrationBuilder.DropTable(
                name: "AppPartidos");
        }
    }
}
