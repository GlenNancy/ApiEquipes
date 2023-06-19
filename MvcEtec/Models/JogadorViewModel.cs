using System.ComponentModel.DataAnnotations;
using MvcEtec.Models;
using MvcEtec.Models.Enum;

namespace MvcEtec.Models
{
  public class JogadorViewModel
  {
    [Key]
    public int Rm { get; set; }
    public string Nome { get; set; }
    public string Turma { get; set; }    
    public virtual EquipeViewModel Equipe { get; set; }

  }
}