namespace e_Ticaret.Application.DTOs.Products
{
    public class ProductPlatformDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PlatformIntegrationId { get; set; }
        public string PlatformName { get; set; } = string.Empty;
        public string? PlatformProductId { get; set; }
        public decimal PlatformPrice { get; set; }
        public int PlatformStockQuantity { get; set; }
        public bool IsListed { get; set; }
        public bool AutoSync { get; set; }
        public DateTime? LastSyncDate { get; set; }
    }
}
