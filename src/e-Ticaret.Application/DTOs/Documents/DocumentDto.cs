using e_Ticaret.Domain.Enums;
using e_Ticaret.Application.DTOs.Auth;

namespace e_Ticaret.Application.DTOs.Documents
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentTypeName { get; set; } = string.Empty;
        public DocumentStatus Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public string? FilePath { get; set; }
        public string? FileExtension { get; set; }
        public long? FileSize { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserDto CreatedByUser { get; set; } = null!;
        public UserDto? UpdatedByUser { get; set; }
    }
}
