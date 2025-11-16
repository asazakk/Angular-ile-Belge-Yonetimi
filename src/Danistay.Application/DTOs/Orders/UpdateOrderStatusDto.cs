using Danistay.Domain.Enums;

namespace Danistay.Application.DTOs.Orders
{
    public class UpdateOrderStatusDto
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public string? Notes { get; set; }
        public string? CargoCompany { get; set; }
        public string? TrackingNumber { get; set; }
    }
}
