using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Application.DTOs.Platforms
{
    public class PlatformIntegrationDto
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public PlatformType PlatformType { get; set; }
        public string PlatformTypeName { get; set; } = string.Empty;
        public string PlatformStoreName { get; set; } = string.Empty;
        public string? SellerId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastSyncDate { get; set; }
        public SyncStatus LastSyncStatus { get; set; }
        public string LastSyncStatusName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
