using e_Ticaret.Domain.Entities;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByStoreIdAsync(int storeId);
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
        Task<Product?> GetWithPlatformsAsync(int productId);
        Task<Product?> GetWithImagesAsync(int productId);
        Task<IEnumerable<Product>> GetLowStockProductsAsync(int storeId);
        Task<IEnumerable<Product>> GetByStockStatusAsync(int storeId, StockStatus status);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm, int storeId);
        Task<Product?> GetBySKUAsync(string sku, int storeId);
        Task<IEnumerable<Product>> GetActiveProductsAsync(int storeId);
    }
}
