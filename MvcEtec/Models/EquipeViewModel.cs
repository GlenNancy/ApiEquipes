using System.ComponentModel.DataAnnotations;
using MvcEtec.Models;
using MvcEtec.Models.Enum;

namespace MvcEtec.Models
{
    public class EquipeViewModel
    {
        [Key]
        public int Id { get; set; }
        public string NomeEquipe { get; set; }
        public virtual ICollection<JogadorViewModel> Jogadores { get; set; }
        public EquipeEnum Classe { get; set; }
        
    }
}