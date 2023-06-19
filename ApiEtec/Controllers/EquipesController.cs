using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEtec.Models;
using ApiEtec.Data;

namespace ApiEtec.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EquipesController : ControllerBase
    {
        private readonly DataContext _context;

        public EquipesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Equipe equipe = await _context.Equipes
                    .Include(e => e.Jogadores)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (equipe == null)
                {
                    return NotFound("A Equipe não foi encontrada.");
                }

                return Ok(equipe);
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
                List<Equipe> lista = await _context.Equipes
                    .Include(e => e.Jogadores)
                    .ToListAsync();

                if (lista == null)
                {
                    return NotFound("Não existe nenhuma equipe criada.");
                }

                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Equipe novaEquipe)
        {
            try
            {
                if (novaEquipe.NomeEquipe.Length <= 2)
                {
                    throw new System.Exception("O nome da Equipe deve ter pelo menos 3 caracteres.");
                }

                await _context.Equipes.AddAsync(novaEquipe);
                await _context.SaveChangesAsync();

                return Ok("Equipe Criada Com Sucesso!");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNome(int id, [FromBody] Equipe equipeAtualizada)
        {
            try
            {
                Equipe equipeExistente = await _context.Equipes.FirstOrDefaultAsync(e => e.Id == id);

                if (equipeExistente == null)
                {
                    return NotFound("Equipe não encontrada.");
                }

                equipeExistente.NomeEquipe = equipeAtualizada.NomeEquipe;

                _context.Equipes.Update(equipeExistente);
                await _context.SaveChangesAsync();

                return Ok("Atualização de nome realizada com sucesso.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Equipe equipeRemover = await _context.Equipes.FirstOrDefaultAsync(e => e.Id == id);

                if (equipeRemover == null)
                {
                    return NotFound("Equipe não encontrada.");
                }

                _context.Equipes.Remove(equipeRemover);
                await _context.SaveChangesAsync();

                return Ok("Equipe removida com sucesso.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{equipeId}/{jogadorRm}")]
        public IActionResult AdicionarJogador(int equipeId, int jogadorRm)
        {
            try
            {
                Equipe equipe = _context.Equipes
                    .Include(e => e.Jogadores)
                    .FirstOrDefault(e => e.Id == equipeId);

                Jogador jogador = _context.Jogadores
                    .FirstOrDefault(j => j.Rm == jogadorRm);

                if (equipe == null || jogador == null)
                {
                    return NotFound("Equipe ou jogador não encontrados.");
                }

                equipe.Jogadores.Add(jogador);
                _context.SaveChanges();

                return Ok("Jogador adicionado à equipe com sucesso.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{equipeId}/jogadores/{jogadorRm}")]
        public async Task<IActionResult> RemoverJogador(int equipeId, int jogadorRm)
        {
            try
            {
                Equipe equipe = await _context.Equipes
                    .Include(e => e.Jogadores)
                    .FirstOrDefaultAsync(e => e.Id == equipeId);

                Jogador jogador = equipe?.Jogadores.FirstOrDefault(j => j.Rm == jogadorRm);

                if (equipe == null || jogador == null)
                {
                    return NotFound("Equipe ou jogador não encontrados.");
                }

                equipe.Jogadores.Remove(jogador);
                await _context.SaveChangesAsync();

                return Ok("Jogador removido da equipe com sucesso.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
