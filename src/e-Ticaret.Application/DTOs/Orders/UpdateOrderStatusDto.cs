using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Application.DTOs.Orders
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
