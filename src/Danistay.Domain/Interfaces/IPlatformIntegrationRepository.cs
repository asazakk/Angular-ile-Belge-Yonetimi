using Danistay.Domain.Entities;
using Danistay.Domain.Enums;

namespace Danistay.Domain.Interfaces
{
    public interface IPlatformIntegrationRepository : IRepository<PlatformIntegration>
    {
        Task<IEnumerable<PlatformIntegration>> GetByStoreIdAsync(int storeId);
        Task<PlatformIntegration?> GetByPlatformTypeAsync(int storeId, PlatformType platformType);
        Task<IEnumerable<PlatformIntegration>> GetActiveIntegrationsAsync();
        Task<IEnumerable<PlatformIntegration>> GetIntegrationsDueForSyncAsync();
    }
}
