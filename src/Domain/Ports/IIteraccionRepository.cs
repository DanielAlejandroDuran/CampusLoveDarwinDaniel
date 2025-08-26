using System.Collections.Generic;
using System.Threading.Tasks;
using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Ports
{
    public interface IInteraccionRepository
    {
        Task<Interaccion> AddAsync(Interaccion interaccion);
        Task<Interaccion?> GetBetweenAsync(int origenId, int destinoId);
        Task<List<Interaccion>> GetByUsuarioAsync(int usuarioId);
        Task<List<(int UsuarioId, int LikesReceived)>> GetLikesReceivedStatsAsync(int topN);
        Task SaveChangesAsync();
    }
}
