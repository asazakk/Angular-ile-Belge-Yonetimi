using e_Ticaret.Domain.Entities;

namespace e_Ticaret.Domain.Interfaces
{
    public interface IProductPlatformRepository : IRepository<ProductPlatform>
    {
        Task<IEnumerable<ProductPlatform>> GetByProductIdAsync(int productId);
        Task<IEnumerable<ProductPlatform>> GetByPlatformIntegrationIdAsync(int platformIntegrationId);
        Task<ProductPlatform?> GetByProductAndPlatformAsync(int productId, int platformIntegrationId);
        Task<IEnumerable<ProductPlatform>> GetListedProductsAsync(int platformIntegrationId);
        Task<IEnumerable<ProductPlatform>> GetProductsDueForSyncAsync();
    }
}
