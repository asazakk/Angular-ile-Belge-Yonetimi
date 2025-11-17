using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_Ticaret.Domain.Entities
{
    public class ProductPlatform : BaseEntity
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int PlatformIntegrationId { get; set; }

        [StringLength(100)]
        public string? PlatformProductId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PlatformPrice { get; set; }

        public int PlatformStockQuantity { get; set; } = 0;

        public bool IsListed { get; set; } = false;

        public bool AutoSync { get; set; } = true;

        public DateTime? LastSyncDate { get; set; }

        // Navigation Properties
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;

        [ForeignKey("PlatformIntegrationId")]
        public virtual PlatformIntegration PlatformIntegration { get; set; } = null!;
    }
}
