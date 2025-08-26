using System.Collections.Generic;
using System.Threading.Tasks;
using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Ports
{
    public interface IUsuarioRepository
    {
        Task<Usuario> AddAsync(Usuario usuario);
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario?> GetByIdNoTrackingAsync(int id);
        Task<List<Usuario>> GetAllAsync();
        Task<List<Usuario>> GetProfilesForAsync(int requestingUserId, int page = 0, int pageSize = 20);
        Task SaveChangesAsync();
    }
}
