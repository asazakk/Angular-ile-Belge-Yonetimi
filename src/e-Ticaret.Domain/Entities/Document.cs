using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Domain.Entities
{
    public class Document : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [Required]
        public DocumentType DocumentType { get; set; }
        
        [Required]
        public DocumentStatus Status { get; set; } = DocumentStatus.Draft;
        
        [StringLength(500)]
        public string? FilePath { get; set; }
        
        [StringLength(100)]
        public string? FileExtension { get; set; }
        
        public long? FileSize { get; set; }
        
        [Required]
        public int CreatedByUserId { get; set; }
        
        public int? UpdatedByUserId { get; set; }
        
        // Navigation Properties
        [ForeignKey("CreatedByUserId")]
        public virtual User CreatedByUser { get; set; } = null!;
        
        [ForeignKey("UpdatedByUserId")]
        public virtual User? UpdatedByUser { get; set; }
        
        public virtual ICollection<DocumentAction> DocumentActions { get; set; } = new List<DocumentAction>();
    }
}
