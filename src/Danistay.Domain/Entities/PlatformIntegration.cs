using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Danistay.Domain.Enums;

namespace Danistay.Domain.Entities
{
    public class PlatformIntegration : BaseEntity
    {
        [Required]
        public int StoreId { get; set; }

        [Required]
        public PlatformType PlatformType { get; set; }

        [Required]
        [StringLength(200)]
        public string PlatformStoreName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ApiKey { get; set; }

        [StringLength(500)]
        public string? ApiSecret { get; set; }

        [StringLength(500)]
        public string? AccessToken { get; set; }

        [StringLength(100)]
        public string? SellerId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LastSyncDate { get; set; }

        public SyncStatus LastSyncStatus { get; set; } = SyncStatus.Pending;

        // Navigation Properties
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<ProductPlatform> ProductPlatforms { get; set; } = new List<ProductPlatform>();
    }
}
