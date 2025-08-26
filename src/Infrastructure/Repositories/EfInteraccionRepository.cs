using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using CampusLove.Infrastructure.Persistence;

namespace CampusLove.Infrastructure.Repositories
{
    public class EfInteraccionRepository : IInteraccionRepository
    {
        private readonly AppDbContext _db;
        public EfInteraccionRepository(AppDbContext db) => _db = db;

        public async Task<Interaccion> AddAsync(Interaccion interaccion)
        {
            _db.Interacciones.Add(interaccion);
            await _db.SaveChangesAsync();
            return interaccion;
        }

        public async Task<Interaccion?> GetBetweenAsync(int origenId, int destinoId) =>
            await _db.Interacciones.FirstOrDefaultAsync(i => i.UsuarioOrigenId == origenId && i.UsuarioDestinoId == destinoId);

        public async Task<List<Interaccion>> GetByUsuarioAsync(int usuarioId) =>
            await _db.Interacciones.Where(i => i.UsuarioOrigenId == usuarioId || i.UsuarioDestinoId == usuarioId).ToListAsync();

        public async Task<List<(int UsuarioId, int LikesReceived)>> GetLikesReceivedStatsAsync(int topN)
        {
            var q = await _db.Interacciones
                .Where(i => i.TipoInteraccion == TipoInteraccion.Like)
                .GroupBy(i => i.UsuarioDestinoId)
                .Select(g => new { UsuarioId = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(topN)
                .ToListAsync();

            return q.Select(x => (x.UsuarioId, x.Count)).ToList();
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
