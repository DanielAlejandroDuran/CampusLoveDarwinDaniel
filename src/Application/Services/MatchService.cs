using System;
using System.Linq;
using System.Threading.Tasks;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using CampusLove.Shared.Helpers;
using System.Globalization;
using System.Collections.Generic;

namespace CampusLove.Application.Services
{
    public class MatchService
    {
        private readonly IUsuarioRepository _usuarios;
        private readonly IInteraccionRepository _interacciones;
        private const int MAX_CREDITOS_POR_DIA = 5;

        public MatchService(IUsuarioRepository usuarios, IInteraccionRepository interacciones)
        {
            _usuarios = usuarios;
            _interacciones = interacciones;
        }

        public async Task<Usuario> RegisterUserAsync(string nombre, int edad, string genero, string carrera, string? frase, IEnumerable<string> intereses, string? password)
        {
            var usuario = new Usuario
            {
                Nombre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((nombre ?? string.Empty).Trim().ToLower()),
                Edad = edad,
                Genero = genero,
                Carrera = carrera,
                FrasePerfil = frase,
                CreditosDisponibles = MAX_CREDITOS_POR_DIA,
                UltimaRecargaCreditos = DateTime.UtcNow.Date
            };

            foreach (var i in intereses.Where(x => !string.IsNullOrWhiteSpace(x)))
                usuario.Intereses.Add(new InteresUsuario { Interes = i.Trim() });

            if (!string.IsNullOrWhiteSpace(password))
            {
                usuario.PasswordHash = PasswordHelper.HashPassword(password);
            }

            return await _usuarios.AddAsync(usuario);
        }

        public async Task<(bool Success, string Message, Usuario? Usuario)> AuthenticateAsync(int usuarioId, string? password)
        {
            var u = await _usuarios.GetByIdAsync(usuarioId);
            if (u == null) return (false, "Usuario no encontrado.", null);

            if (string.IsNullOrEmpty(u.PasswordHash))
            {
                // Modo legacy: permitir acceso sin contraseña
                return (true, "Usuario sin contraseña (acceso legacy permitido).", u);
            }

            if (string.IsNullOrWhiteSpace(password))
                return (false, "Contraseña requerida.", null);

            var ok = PasswordHelper.Verify(password, u.PasswordHash!);
            if (!ok) return (false, "Contraseña incorrecta.", null);

            return (true, "Autenticación correcta.", u);
        }

        public async Task<(bool Success, string Message, Usuario? Usuario)> AuthenticateByNameAsync(string nombre, string? password)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return (false, "El nombre es requerido.", null);
            
            var u = await _usuarios.GetByNameAsync(nombre.Trim());
            if (u == null) return (false, "Usuario no encontrado.", null);

            if (string.IsNullOrEmpty(u.PasswordHash))
            {
                // Modo legacy: permitir acceso sin contraseña
                return (true, "Usuario sin contraseña (acceso legacy permitido).", u);
            }

            if (string.IsNullOrWhiteSpace(password))
                return (false, "Contraseña requerida.", null);

            var ok = PasswordHelper.Verify(password, u.PasswordHash!);
            if (!ok) return (false, "Contraseña incorrecta.", null);

            return (true, $"¡Bienvenido {u.Nombre}!", u);
        }

        private void EnsureCreditosRecargados(Usuario u)
        {
            if (u.UltimaRecargaCreditos.Date < DateTime.UtcNow.Date)
            {
                u.CreditosDisponibles = MAX_CREDITOS_POR_DIA;
                u.UltimaRecargaCreditos = DateTime.UtcNow.Date;
            }
        }

        public async Task<(bool Success, string Message)> LikeAsync(int origenId, int destinoId)
        {
            if (origenId == destinoId) return (false, "No puedes interactuar contigo mismo.");
            var origen = await _usuarios.GetByIdAsync(origenId);
            var destino = await _usuarios.GetByIdAsync(destinoId);
            if (origen == null || destino == null) return (false, "Usuario no encontrado.");

            EnsureCreditosRecargados(origen);
            if (origen.CreditosDisponibles <= 0) return (false, "No tienes créditos disponibles hoy.");

            var existing = await _interacciones.GetBetweenAsync(origenId, destinoId);
            if (existing != null) return (false, "Ya interactuaste con este usuario.");

            var inter = new Interaccion
            {
                UsuarioOrigenId = origenId,
                UsuarioDestinoId = destinoId,
                TipoInteraccion = TipoInteraccion.Like
            };

            await _interacciones.AddAsync(inter);

            origen.CreditosDisponibles = Math.Max(0, origen.CreditosDisponibles - 1);
            await _usuarios.SaveChangesAsync();

            var reciprocal = await _interacciones.GetBetweenAsync(destinoId, origenId);
            if (reciprocal != null && reciprocal.TipoInteraccion == TipoInteraccion.Like)
            {
                // Si quieres persistir matches en tabla separada, aquí es donde lo harías.
                return (true, "¡Es un MATCH!");
            }

            return (true, "Like registrado.");
        }

        public async Task<(bool Success, string Message)> DislikeAsync(int origenId, int destinoId)
        {
            if (origenId == destinoId) return (false, "No puedes interactuar contigo mismo.");
            var existing = await _interacciones.GetBetweenAsync(origenId, destinoId);
            if (existing != null) return (false, "Ya interactuaste con este usuario.");

            var inter = new Interaccion
            {
                UsuarioOrigenId = origenId,
                UsuarioDestinoId = destinoId,
                TipoInteraccion = TipoInteraccion.Dislike
            };

            await _interacciones.AddAsync(inter);
            return (true, "Dislike registrado.");
        }

        public async Task<List<Usuario>> GetMatchesForAsync(int usuarioId)
        {
            var sentLikes = (await _interacciones.GetByUsuarioAsync(usuarioId))
                .Where(i => i.UsuarioOrigenId == usuarioId && i.TipoInteraccion == TipoInteraccion.Like)
                .Select(i => i.UsuarioDestinoId)
                .ToHashSet();

            var receivedLikes = (await _interacciones.GetByUsuarioAsync(usuarioId))
                .Where(i => i.UsuarioDestinoId == usuarioId && i.TipoInteraccion == TipoInteraccion.Like)
                .Select(i => i.UsuarioOrigenId)
                .Where(id => sentLikes.Contains(id))
                .ToList();

            var matches = new List<Usuario>();
            foreach (var id in receivedLikes)
            {
                var u = await _usuarios.GetByIdAsync(id);
                if (u != null) matches.Add(u);
            }
            return matches;
        }

        public async Task<List<(int UsuarioId,int LikesReceived)>> GetTopLikedAsync(int topN = 5)
        {
            return await _interacciones.GetLikesReceivedStatsAsync(topN);
        }
    }
}
