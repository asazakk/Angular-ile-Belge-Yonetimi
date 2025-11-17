using e_Ticaret.Application.DTOs.Products;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(int storeId);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto?> GetProductWithPlatformsAsync(int id);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<ProductDto>> GetLowStockProductsAsync(int storeId);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm, int storeId);
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto, int currentUserId);
        Task<ProductDto?> UpdateProductAsync(UpdateProductDto updateProductDto, int currentUserId);
        Task<bool> DeleteProductAsync(int id, int currentUserId);
        Task<bool> UpdateStockAsync(int productId, int quantity, string changeType, string? reason = null);
        Task<bool> UpdatePriceAsync(int productId, decimal newPrice, string changeType, string? reason = null);
        Task<bool> SyncProductToPlatformAsync(int productId, int platformIntegrationId);
        Task<bool> SyncAllProductsAsync(int storeId);
    }
}
