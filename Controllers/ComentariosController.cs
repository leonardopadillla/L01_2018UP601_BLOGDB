using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2018UP601_BLOGDB.Models;

namespace L01_2018UP601_BLOGDB.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly BlogDbContext _context;

        public ComentariosController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: api/Comentarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentarios()
        {
            return await _context.Comentarios.ToListAsync();
        }

        // GET: api/Comentarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> GetComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);

            if (comentario == null)
            {
                return NotFound();
            }

            return comentario;
        }

        // POST: api/Comentarios
        [HttpPost]
        public async Task<ActionResult<Comentario>> PostComentario(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComentario", new { id = comentario.comentarioId }, comentario);
        }

        // PUT: api/Comentarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentario(int id, Comentario comentario)
        {
            if (id != comentario.comentarioId)
            {
                return BadRequest();
            }

            _context.Entry(comentario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExists(id))
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

        // DELETE: api/Comentarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Comentarios/ByPublicacion/5
        [HttpGet("ByPublicacion/{id}")]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentariosByPublicacion(int id)
        {
            var comentarios = await _context.Comentarios.Where(c => c.publicacionId == id).ToListAsync();

            if (comentarios == null || comentarios.Count == 0)
            {
                return NotFound();
            }

            return comentarios;
        }

        private bool ComentarioExists(int id)
        {
            return _context.Comentarios.Any(e => e.comentarioId == id);
        }
    }
}