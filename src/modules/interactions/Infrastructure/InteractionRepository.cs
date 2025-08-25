using CampusLoveDarwinDaniel.Modules.Interactions.Domain.Entities;
using CampusLoveDarwinDaniel.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace CampusLoveDarwinDaniel.Modules.Interactions.Infrastructure.Repositories
{
    public class InteractionRepository
    {
        private readonly CampusLoveDbContext _context;

        public InteractionRepository(CampusLoveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Interaction>> GetAllAsync()
        {
            return await _context.Interactions.ToListAsync();
        }

        public async Task AddAsync(Interaction interaction)
        {
            _context.Interactions.Add(interaction);
            await _context.SaveChangesAsync();
        }
    }
}
