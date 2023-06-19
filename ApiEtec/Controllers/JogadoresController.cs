using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEtec.Models;
using ApiEtec.Data;

namespace ApiEtec.Controllers
{
  [ApiController]
  [Route("[Controller]")]
  public class JogadoresController : ControllerBase
  {
    private readonly DataContext _context;

    public JogadoresController(DataContext context)
    {
      _context = context;
    }
    [HttpGet("{rm}")]
    public async Task<IActionResult> GetSingle(int rm)
    {
      try
      {
        Jogador j = await _context.Jogadores
            .FirstOrDefaultAsync(pBusca => pBusca.Rm == rm);

        if (j == null)
        {
          return NotFound("O jogador não foi encontrado.");
        }

        return Ok(j);
      }
      catch (System.Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> Get()
    {
      try
      {
        List<Jogador> lista = await _context.Jogadores.ToListAsync();
        return Ok(lista);
      }
      catch (System.Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
    [HttpPost]
    public async Task<IActionResult> Add(Jogador novoJogador)
    {
      try
      {
        if (novoJogador.Nome.Length <= 2)
        {
          throw new System.Exception("O nome deve ter pelo menos 3 caracteres.");
        }
        await _context.Jogadores.AddAsync(novoJogador);
        await _context.SaveChangesAsync();

        return Ok(novoJogador.Rm);
      }
      catch (System.Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
    [HttpPut("{rm}")]
    public async Task<IActionResult> Update(int rm, [FromBody] Jogador jogadorAtualizado)
    {
      try
      {
        Jogador jogadorExistente = await _context.Jogadores.FirstOrDefaultAsync(j => j.Rm == rm);

        if (jogadorExistente == null)
        {
          return NotFound("Jogador não encontrado.");
        }

        // Atualizar apenas a propriedade Turma
        jogadorExistente.Turma = jogadorAtualizado.Turma;

        _context.Jogadores.Update(jogadorExistente);
        await _context.SaveChangesAsync();

        return Ok("Atualização de turma realizada com sucesso.");
      }
      catch (System.Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
    [HttpDelete("{rm}")]
    public async Task<IActionResult> Delete(int rm)
    {
      try
      {
        Jogador pRemover = await _context.Jogadores.FirstOrDefaultAsync(p => p.Rm == rm);

        if(pRemover == null)
        {
            return NotFound("Jogador não encontrado.");
        }

        _context.Jogadores.Remove(pRemover);
        await _context.SaveChangesAsync();
        return Ok("Jogador Removido Com Sucesso");
      }
      catch (System.Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}