using System;
using System.Collections.Generic;

namespace CampusLove.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; } = null!;
        public int Edad { get; set; }
        public string Genero { get; set; } = null!;
        public string Carrera { get; set; } = null!;
        public string? FrasePerfil { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public bool EstaActivo { get; set; } = true;
        public int CreditosDisponibles { get; set; } = 5;
        public DateTime UltimaRecargaCreditos { get; set; } = DateTime.UtcNow.Date;

        // Hash de contraseña (nullable para compatibilidad con registros legacy)
        public string? PasswordHash { get; set; }

        public ICollection<InteresUsuario> Intereses { get; set; } = new List<InteresUsuario>();
    }
}
