using Microsoft.EntityFrameworkCore;
using ApiEtec.Models;
using ApiEtec.Models.Enum;
using ApiEtec.Controllers;

namespace ApiEtec.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<Equipe> Equipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jogador>().HasData(
                new Jogador() { Rm = 1, Nome = "Jogador Teste1", Turma = "2º ano"},
                new Jogador() { Rm = 2, Nome = "Jogador Teste2", Turma = "3º ano"}
            );
            modelBuilder.Entity<Equipe>().HasData
            (
                new Equipe() { Id = 1, NomeEquipe = "Bastard" },
                new Equipe() { Id = 2, NomeEquipe = "Pxg" }
            );
        }
    }
}