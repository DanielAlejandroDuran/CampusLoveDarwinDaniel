using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using CampusLove.Infrastructure.Persistence;

namespace CampusLove.Infrastructure.Repositories
{
    public class EfUsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _db;
        public EfUsuarioRepository(AppDbContext db) => _db = db;

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario?> GetByIdAsync(int id) =>
            await _db.Usuarios.Include(u => u.Intereses).FirstOrDefaultAsync(u => u.UsuarioId == id);

        public async Task<Usuario?> GetByIdNoTrackingAsync(int id) =>
            await _db.Usuarios.AsNoTracking().Include(u => u.Intereses).FirstOrDefaultAsync(u => u.UsuarioId == id);

        public async Task<Usuario?> GetByNameAsync(string nombre) =>
            await _db.Usuarios.Include(u => u.Intereses).FirstOrDefaultAsync(u => u.Nombre == nombre);

        public async Task<List<Usuario>> GetAllAsync() =>
            await _db.Usuarios.Include(u => u.Intereses).ToListAsync();

        public async Task<List<Usuario>> GetProfilesForAsync(int requestingUserId, int page = 0, int pageSize = 20)
        {
            var interactedWith = await _db.Interacciones
                .Where(i => i.UsuarioOrigenId == requestingUserId)
                .Select(i => i.UsuarioDestinoId)
                .ToListAsync();

            var query = _db.Usuarios
                .Where(u => u.UsuarioId != requestingUserId && !interactedWith.Contains(u.UsuarioId) && u.EstaActivo)
                .Include(u => u.Intereses)
                .OrderBy(u => u.UsuarioId)
                .Skip(page * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}

