using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Application.DTOs.Products
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? SKU { get; set; }
        public string? Barcode { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int StoreId { get; set; }
        public decimal BasePrice { get; set; }
        public decimal? CostPrice { get; set; }
        public int StockQuantity { get; set; }
        public int MinStockLevel { get; set; } = 5;
        public string? Weight { get; set; }
        public string? Dimensions { get; set; }
        public string? MainImageUrl { get; set; }
        public PriceStrategy PriceStrategy { get; set; } = PriceStrategy.Fixed;
        public List<string>? ImageUrls { get; set; }
    }
}
