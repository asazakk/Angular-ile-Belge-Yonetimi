using e_Ticaret.Application.DTOs.Stores;

namespace e_Ticaret.Application.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreDto>> GetAllStoresAsync(int userId);
        Task<StoreDto?> GetStoreByIdAsync(int id);
        Task<StoreDto> CreateStoreAsync(CreateStoreDto createStoreDto, int currentUserId);
        Task<StoreDto?> UpdateStoreAsync(UpdateStoreDto updateStoreDto, int currentUserId);
        Task<bool> DeleteStoreAsync(int id, int currentUserId);
        Task<IEnumerable<StoreDto>> GetActiveStoresAsync(int userId);
    }
}
