using CampusLoveDarwinDaniel.Modules.Matches.Domain.Entities;
using CampusLoveDarwinDaniel.Modules.Matches.Infrastructure.Repositories;

namespace CampusLoveDarwinDaniel.Modules.Matches.Application.Services
{
    public class MatchService
    {
        private readonly MatchRepository _matchRepository;

        public MatchService(MatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public async Task<IEnumerable<Match>> GetAllMatchesAsync()
        {
            return await _matchRepository.GetAllAsync();
        }

        public async Task AddMatchAsync(Match match)
        {
            await _matchRepository.AddAsync(match);
        }
    }
}
