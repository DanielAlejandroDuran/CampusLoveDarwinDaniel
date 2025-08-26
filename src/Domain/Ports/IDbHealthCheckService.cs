using System.Threading.Tasks;

namespace CampusLove.Domain.Ports
{
    public interface IDbHealthCheckService
    {
        Task<bool> IsHealthyAsync();
        Task EnsureDatabaseCreatedAsync();
    }
}