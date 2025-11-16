using System.ComponentModel.DataAnnotations;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Application.DTOs.Documents
{
    public class UpdateDocumentDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [Required]
        public DocumentType DocumentType { get; set; }
        
        [Required]
        public DocumentStatus Status { get; set; }
    }
}
