using Danistay.Application.DTOs.Orders;
using Danistay.Domain.Enums;

namespace Danistay.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync(int storeId);
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<OrderDto?> GetOrderWithItemsAsync(int id);
        Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(OrderStatus status);
        Task<IEnumerable<OrderDto>> GetRecentOrdersAsync(int count = 10);
        Task<IEnumerable<OrderDto>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<bool> UpdateOrderStatusAsync(UpdateOrderStatusDto updateOrderStatusDto, int currentUserId);
        Task<bool> SyncOrdersFromPlatformAsync(int platformIntegrationId);
        Task<bool> SyncAllOrdersAsync(int storeId);
        Task<decimal> GetTotalSalesAsync(int storeId, DateTime startDate, DateTime endDate);
        Task<Dictionary<OrderStatus, int>> GetOrderStatisticsAsync(int storeId);
    }
}
