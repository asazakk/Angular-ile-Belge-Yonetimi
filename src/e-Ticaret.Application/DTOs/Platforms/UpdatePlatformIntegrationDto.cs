using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Application.DTOs.Platforms
{
    public class UpdatePlatformIntegrationDto
    {
        public int Id { get; set; }
        public string PlatformStoreName { get; set; } = string.Empty;
        public string? ApiKey { get; set; }
        public string? ApiSecret { get; set; }
        public string? AccessToken { get; set; }
        public string? SellerId { get; set; }
        public bool IsActive { get; set; }
    }
}
