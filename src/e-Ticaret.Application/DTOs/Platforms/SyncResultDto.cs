using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Application.DTOs.Platforms
{
    public class SyncResultDto
    {
        public bool Success { get; set; }
        public SyncStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? ProductsSynced { get; set; }
        public int? OrdersSynced { get; set; }
        public DateTime SyncDate { get; set; }
        public List<string>? Errors { get; set; }
    }
}
