using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    
using EstoqueAPI.Data;
using EstoqueAPI.Models;    

namespace EstoqueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstoqueController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("testarconexao")]
        public IActionResult TestarConexao()
        {
            if (_context.Database.CanConnect())
            {
                return Ok("Conexão com o banco de dados está funcionando!");
            }
            else
            {
                return StatusCode(500, "Não foi possível conectar ao banco de dados.");
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemEstoque>>> GetEstoque()
        {
            return await _context.Estoque.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemEstoque>> GetEstoque(int id){
            var itemEstoque = await _context.Estoque.FindAsync(id);

            if (itemEstoque == null)
            {
                return NotFound();
            }

            return itemEstoque;
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