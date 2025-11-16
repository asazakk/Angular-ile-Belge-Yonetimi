using Danistay.Domain.Enums;

namespace Danistay.Application.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? SKU { get; set; }
        public string? Barcode { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public decimal? CostPrice { get; set; }
        public int StockQuantity { get; set; }
        public int MinStockLevel { get; set; }
        public StockStatus StockStatus { get; set; }
        public string StockStatusName { get; set; } = string.Empty;
        public string? Weight { get; set; }
        public string? Dimensions { get; set; }
        public string? MainImageUrl { get; set; }
        public bool IsActive { get; set; }
        public PriceStrategy PriceStrategy { get; set; }
        public string PriceStrategyName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<ProductPlatformDto>? Platforms { get; set; }
        public List<ProductImageDto>? Images { get; set; }
    }
}
