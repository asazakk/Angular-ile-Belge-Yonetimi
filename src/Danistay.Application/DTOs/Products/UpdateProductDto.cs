using Danistay.Domain.Enums;

namespace Danistay.Application.DTOs.Products
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? SKU { get; set; }
        public string? Barcode { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public decimal BasePrice { get; set; }
        public decimal? CostPrice { get; set; }
        public int StockQuantity { get; set; }
        public int MinStockLevel { get; set; }
        public string? Weight { get; set; }
        public string? Dimensions { get; set; }
        public string? MainImageUrl { get; set; }
        public bool IsActive { get; set; }
        public PriceStrategy PriceStrategy { get; set; }
    }
}
