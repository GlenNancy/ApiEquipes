using System.ComponentModel.DataAnnotations;

namespace ApiEtec.Models
{
  public class Jogador
  {
    [Key]
    public int Rm { get; set; }
    public string Nome { get; set; }
    public string Turma { get; set; }    
    public virtual Equipe Equipe { get; set; }
    
  }
}