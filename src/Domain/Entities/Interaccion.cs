using System;

namespace CampusLove.Domain.Entities
{
    public enum TipoInteraccion
    {
        Like,
        Dislike
    }

    public class Interaccion
    {
        public int InteraccionId { get; set; }
        public int UsuarioOrigenId { get; set; }
        public int UsuarioDestinoId { get; set; }
        public TipoInteraccion TipoInteraccion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public bool EstaActivo { get; set; } = true;
    }
}
