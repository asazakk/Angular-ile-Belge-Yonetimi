using Danistay.Domain.Enums;

namespace Danistay.Application.DTOs.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public int PlatformIntegrationId { get; set; }
        public string PlatformName { get; set; } = string.Empty;
        public string? PlatformOrderId { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public decimal? ShippingAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; }
        public string? CargoCompany { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto>? OrderItems { get; set; }
    }
}
