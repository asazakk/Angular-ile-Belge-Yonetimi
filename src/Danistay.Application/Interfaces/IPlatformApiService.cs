using Danistay.Domain.Enums;

namespace Danistay.Application.Interfaces
{
    public interface IPlatformApiService
    {
        PlatformType PlatformType { get; }
        
        Task<bool> TestConnectionAsync(string apiKey, string apiSecret, string? sellerId = null);
        Task<bool> PublishProductAsync(int productId, string apiKey, string apiSecret);
        Task<bool> UpdateProductPriceAsync(string platformProductId, decimal price, string apiKey, string apiSecret);
        Task<bool> UpdateProductStockAsync(string platformProductId, int stock, string apiKey, string apiSecret);
        Task<List<object>> FetchOrdersAsync(string apiKey, string apiSecret, DateTime? fromDate = null);
        Task<bool> UpdateOrderStatusAsync(string platformOrderId, string status, string apiKey, string apiSecret);
    }
}
