using CampusLoveDarwinDaniel.Modules.Interactions.Domain.Entities;
using CampusLoveDarwinDaniel.Modules.Interactions.Infrastructure.Repositories;

namespace CampusLoveDarwinDaniel.Modules.Interactions.Application.Services
{
    public class InteractionService
    {
        private readonly InteractionRepository _interactionRepository;

        public InteractionService(InteractionRepository interactionRepository)
        {
            _interactionRepository = interactionRepository;
        }

        public async Task<IEnumerable<Interaction>> GetAllInteractionsAsync()
        {
            return await _interactionRepository.GetAllAsync();
        }

        public async Task AddInteractionAsync(Interaction interaction)
        {
            await _interactionRepository.AddAsync(interaction);
        }
    }
}
