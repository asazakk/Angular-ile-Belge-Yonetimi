using e_Ticaret.Application.DTOs.Platforms;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Application.Interfaces
{
    public interface IPlatformIntegrationService
    {
        Task<IEnumerable<PlatformIntegrationDto>> GetByStoreIdAsync(int storeId);
        Task<PlatformIntegrationDto?> GetByIdAsync(int id);
        Task<PlatformIntegrationDto> CreatePlatformIntegrationAsync(CreatePlatformIntegrationDto dto, int currentUserId);
        Task<PlatformIntegrationDto?> UpdatePlatformIntegrationAsync(UpdatePlatformIntegrationDto dto, int currentUserId);
        Task<bool> DeletePlatformIntegrationAsync(int id, int currentUserId);
        Task<bool> TestConnectionAsync(int platformIntegrationId);
        Task<SyncResultDto> SyncProductsAsync(int platformIntegrationId);
        Task<SyncResultDto> SyncOrdersAsync(int platformIntegrationId);
        Task<SyncResultDto> FullSyncAsync(int platformIntegrationId);
    }
}
