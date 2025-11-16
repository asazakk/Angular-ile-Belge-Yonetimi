using Danistay.Domain.Entities;
using Danistay.Domain.Enums;

namespace Danistay.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetByPlatformIntegrationIdAsync(int platformIntegrationId);
        Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
        Task<Order?> GetWithItemsAsync(int orderId);
        Task<Order?> GetByOrderNumberAsync(string orderNumber);
        Task<IEnumerable<Order>> GetRecentOrdersAsync(int count);
        Task<IEnumerable<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalSalesAsync(int platformIntegrationId, DateTime startDate, DateTime endDate);
        Task<int> GetOrderCountByStatusAsync(OrderStatus status);
    }
}
