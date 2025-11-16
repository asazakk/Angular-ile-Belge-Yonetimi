using Danistay.Domain.Enums;

namespace Danistay.Application.DTOs.Platforms
{
    public class CreatePlatformIntegrationDto
    {
        public int StoreId { get; set; }
        public PlatformType PlatformType { get; set; }
        public string PlatformStoreName { get; set; } = string.Empty;
        public string? ApiKey { get; set; }
        public string? ApiSecret { get; set; }
        public string? AccessToken { get; set; }
        public string? SellerId { get; set; }
    }
}
