using Microsoft.AspNetCore.Http;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Application.DTOs.Document
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
        public string? FileSizeFormatted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedByUserName { get; set; } = string.Empty;
        public string? UpdatedByUserName { get; set; }
    }
    
    public class CreateDocumentDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DocumentType DocumentType { get; set; }
        public IFormFile? File { get; set; }
    }
    
    public class UpdateDocumentDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DocumentType DocumentType { get; set; }
        public DocumentStatus Status { get; set; }
        public IFormFile? File { get; set; }
    }
    
    public class DocumentListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DocumentType DocumentType { get; set; }
        public string DocumentTypeName { get; set; } = string.Empty;
        public DocumentStatus Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string CreatedByUserName { get; set; } = string.Empty;
        public string? FileSizeFormatted { get; set; }
    }
    
    public class DocumentActionDto
    {
        public int Id { get; set; }
        public ActionType ActionType { get; set; }
        public string ActionTypeName { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public DateTime ActionDate { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
