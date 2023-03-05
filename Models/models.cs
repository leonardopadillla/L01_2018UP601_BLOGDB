using System.ComponentModel.DataAnnotations;
namespace L01_2018UP601_BLOGDB.Models
{
    public class Publicacion
    {
        public int publicacionId { get; set; }
        public int usuarioId { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }

    public class Comentario
    {
        public int comentarioId { get; set; }
        public int usuarioId { get; set; }
        public int publicacionId { get; set; }
        public string comentario { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Publicacion Publicacion { get; set; }
    }

    public class Usuario
    {
        public int usuarioId { get; set; }
        public int rolId { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string nombreUsuario { get; set; }
        public string clave { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual ICollection<Publicacion> Publicaciones { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }

    public class Rol
    {
        public int rolId { get; set; }
        public string rol { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
