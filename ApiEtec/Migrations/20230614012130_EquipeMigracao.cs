using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiEtec.Migrations
{
    /// <inheritdoc />
    public partial class EquipeMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEquipe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jogadores",
                columns: table => new
                {
                    Rm = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Turma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogadores", x => x.Rm);
                    table.ForeignKey(
                        name: "FK_Jogadores_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Equipes",
                columns: new[] { "Id", "NomeEquipe" },
                values: new object[,]
                {
                    { 1, "Bastard" },
                    { 2, "Pxg" }
                });

            migrationBuilder.InsertData(
                table: "Jogadores",
                columns: new[] { "Rm", "EquipeId", "Nome", "Turma" },
                values: new object[,]
                {
                    { 1, null, "Jogador Teste1", "2º ano" },
                    { 2, null, "Jogador Teste2", "3º ano" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_EquipeId",
                table: "Jogadores",
                column: "EquipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jogadores");

            migrationBuilder.DropTable(
                name: "Equipes");
        }
    }
}
