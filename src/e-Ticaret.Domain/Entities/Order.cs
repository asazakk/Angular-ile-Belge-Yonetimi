using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Domain.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; } = string.Empty;

        [Required]
        public int PlatformIntegrationId { get; set; }

        [StringLength(100)]
        public string? PlatformOrderId { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ShippingAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TaxAmount { get; set; }

        [StringLength(200)]
        public string? CustomerName { get; set; }

        [StringLength(200)]
        public string? CustomerEmail { get; set; }

        [StringLength(20)]
        public string? CustomerPhone { get; set; }

        [StringLength(1000)]
        public string? ShippingAddress { get; set; }

        [StringLength(1000)]
        public string? BillingAddress { get; set; }

        [StringLength(100)]
        public string? CargoCompany { get; set; }

        [StringLength(100)]
        public string? TrackingNumber { get; set; }

        public DateTime? ShippedDate { get; set; }

        public DateTime? DeliveredDate { get; set; }

        [StringLength(2000)]
        public string? Notes { get; set; }

        // Navigation Properties
        [ForeignKey("PlatformIntegrationId")]
        public virtual PlatformIntegration PlatformIntegration { get; set; } = null!;

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
