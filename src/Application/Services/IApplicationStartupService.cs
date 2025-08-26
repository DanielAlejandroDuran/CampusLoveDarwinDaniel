using System.Threading.Tasks;

namespace CampusLove.Application.Services
{
    public interface IApplicationStartupService
    {
        Task InitializeAsync();
    }
}