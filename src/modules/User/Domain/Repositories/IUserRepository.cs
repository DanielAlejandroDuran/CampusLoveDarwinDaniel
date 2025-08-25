using CampusLoveDarwinDaniel.Modules.Users.Domain.Entities;

namespace CampusLoveDarwinDaniel.Modules.Users.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
