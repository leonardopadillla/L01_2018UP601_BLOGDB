using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L01_2018UP601_BLOGDB.Models;

namespace L01_2018UP601_BLOGDB.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BlogDbContext _context;

        public UsuariosController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            return _context.Usuarios.ToList();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // POST: api/Usuarios
        [HttpPost]
        public ActionResult<Usuario> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.usuarioId }, usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public IActionResult PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.usuarioId)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public ActionResult<Usuario> DeleteUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return usuario;
        }

        // GET: api/Usuarios/FiltrarPorNombreApellido?nombre=nombre&apellido=apellido
        [HttpGet("FiltrarPorNombreApellido")]
        public ActionResult<IEnumerable<Usuario>> FiltrarPorNombreApellido(string nombre, string apellido)
        {
            var usuarios = _context.Usuarios.Where(u => u.nombre.Contains(nombre) && u.apellido.Contains(apellido)).ToList();

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        // GET: api/Usuarios/FiltrarPorRol?rol=rol
        [HttpGet("FiltrarPorRol")]
        public ActionResult<IEnumerable<Usuario>> FiltrarPorRol(string rol)
        {
            var usuarios = _context.Usuarios.Include(u => u.Rol).Where(u => u.Rol.rol == rol).ToList();

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.usuarioId == id);
        }
    }
}
