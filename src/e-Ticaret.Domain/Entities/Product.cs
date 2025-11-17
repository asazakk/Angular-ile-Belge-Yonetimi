using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(300)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? SKU { get; set; }

        [StringLength(100)]
        public string? Barcode { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int StoreId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CostPrice { get; set; }

        [Required]
        public int StockQuantity { get; set; } = 0;

        public int MinStockLevel { get; set; } = 5;

        public StockStatus StockStatus { get; set; } = StockStatus.InStock;

        [StringLength(50)]
        public string? Weight { get; set; }

        [StringLength(50)]
        public string? Dimensions { get; set; }

        [StringLength(500)]
        public string? MainImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public PriceStrategy PriceStrategy { get; set; } = PriceStrategy.Fixed;

        // Navigation Properties
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } = null!;

        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; } = null!;

        public virtual ICollection<ProductPlatform> ProductPlatforms { get; set; } = new List<ProductPlatform>();
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<StockHistory> StockHistories { get; set; } = new List<StockHistory>();
        public virtual ICollection<PriceHistory> PriceHistories { get; set; } = new List<PriceHistory>();
    }
}
