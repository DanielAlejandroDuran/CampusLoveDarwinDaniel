using CampusLoveDarwinDaniel.Modules.Matches.Domain.Entities;
using CampusLoveDarwinDaniel.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace CampusLoveDarwinDaniel.Modules.Matches.Infrastructure.Repositories
{
    public class MatchRepository
    {
        private readonly CampusLoveDbContext _context;

        public MatchRepository(CampusLoveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            return await _context.Matches.ToListAsync();
        }

        public async Task AddAsync(Match match)
        {
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
        }
    }
}
