using e_Ticaret.Domain.Entities;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Domain.Interfaces
{
    public interface IPlatformIntegrationRepository : IRepository<PlatformIntegration>
    {
        Task<IEnumerable<PlatformIntegration>> GetByStoreIdAsync(int storeId);
        Task<PlatformIntegration?> GetByPlatformTypeAsync(int storeId, PlatformType platformType);
        Task<IEnumerable<PlatformIntegration>> GetActiveIntegrationsAsync();
        Task<IEnumerable<PlatformIntegration>> GetIntegrationsDueForSyncAsync();
    }
}
