using e_Ticaret.Domain.Entities;

namespace e_Ticaret.Domain.Interfaces
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<IEnumerable<Store>> GetByUserIdAsync(int userId);
        Task<Store?> GetWithPlatformsAsync(int storeId);
        Task<IEnumerable<Store>> GetActiveStoresAsync();
    }
}
