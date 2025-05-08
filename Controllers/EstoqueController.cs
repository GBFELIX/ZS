using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstoqueAPI.Data;
using EstoqueAPI.Models;

namespace EstoqueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // Testa a conexão com o banco de dados
        [HttpGet("testarconexao")]
        public IActionResult TestarConexao()
        {
            try
            {
                if (_context.Database.CanConnect())
                {
                    return Ok("Conexão com o banco de dados está certa. :D");
                }
                else
                {
                    return StatusCode(500, "Não conectou com o banco de dados. D:");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao conectar com o banco de dados: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemEstoque>>> GetEstoque()
        {
            var estoque = await _context.Estoque.Include(i => i.Categoria).ToListAsync();
            return Ok(estoque);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemEstoque>> GetEstoque(int id)
        {
            var itemEstoque = await _context.Estoque.Include(i => i.Categoria).FirstOrDefaultAsync(i => i.Id == id);

            if (itemEstoque == null)
            {
                return NotFound();
            }

            return Ok(itemEstoque);
        }

        [HttpPost]
        public async Task<ActionResult<ItemEstoque>> PostEstoque(ItemEstoque itemEstoque)
        {
            _context.Estoque.Add(itemEstoque);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstoque), new { id = itemEstoque.Id }, itemEstoque);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstoque(int id, ItemEstoque itemEstoque)
        {
            if (id != itemEstoque.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemEstoque).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemEstoqueExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstoque(int id)
        {
            var itemEstoque = await _context.Estoque.FindAsync(id);
            if (itemEstoque == null)
            {
                return NotFound();
            }

            _context.Estoque.Remove(itemEstoque);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool ItemEstoqueExists(int id)
        {
            return _context.Estoque.Any(e => e.Id == id);
        }
    }
}
