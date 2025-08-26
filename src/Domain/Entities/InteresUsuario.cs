using System;

namespace CampusLove.Domain.Entities
{
    public class InteresUsuario
    {
        public int InteresUsuarioId { get; set; }
        public int UsuarioId { get; set; }
        public string Interes { get; set; } = null!;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public Usuario? Usuario { get; set; }
    }
}
