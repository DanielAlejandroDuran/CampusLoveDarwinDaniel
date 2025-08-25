using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusLoveDarwinDaniel.Modules.Users.Domain.Entities
{
    /// <summary>
    /// Representa la entidad Usuario, mapeada desde la tabla Usuarios de la base de datos.
    /// Se han agregado atributos para un mapeo explícito y seguro.
    /// </summary>
    [Table("Usuarios")]
    public class User
    {
        [Column("UsuarioId")]
        public int UserId { get; set; }

        [Column("Nombre")]
        public string Name { get; set; }

        [Column("Edad")]
        public int Age { get; set; }

        [Column("Genero")]
        public string Gender { get; set; }

        [Column("Carrera")]
        public string Career { get; set; }

        [Column("FrasePerfil")]
        public string? ProfilePhrase { get; set; }

        [Column("FechaCreacion")]
        public DateTime CreatedAt { get; set; }

        [Column("EstaActivo")]
        public bool IsActive { get; set; }

        [Column("CreditosDisponibles")]
        public int CreditsAvailable { get; set; }

        [Column("UltimaRecargaCreditos")]
        public DateTime LastCreditRecharge { get; set; }

        // Propiedad de navegación para los intereses del usuario.
        // EF Core la asociará con la tabla InteresesUsuario a través de la configuración en DbContext.
        public virtual ICollection<UserInterest> Interests { get; set; } = new List<UserInterest>();
    }
}