using System.ComponentModel.DataAnnotations;
using ApiEtec.Models.Enum;

namespace ApiEtec.Models
{
    public class Equipe
    {
        [Key]
        public int Id { get; set; }
        public string NomeEquipe { get; set; }
        public virtual ICollection<Jogador> Jogadores { get; set; }
        
    }
}