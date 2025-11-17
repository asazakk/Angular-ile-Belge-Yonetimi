using System.ComponentModel.DataAnnotations;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Application.DTOs.Documents
{
    public class CreateDocumentDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [Required]
        public DocumentType DocumentType { get; set; }
        
        // File upload properties
        public string? FileName { get; set; }
        public string? FileExtension { get; set; }
        public long? FileSize { get; set; }
        public byte[]? FileContent { get; set; }
    }
}
