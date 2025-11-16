using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using e_Ticaret.Domain.Enums;

namespace e_Ticaret.Domain.Entities
{
    public class DocumentAction : BaseEntity
    {
        [Required]
        public int DocumentId { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public ActionType ActionType { get; set; }
        
        [StringLength(500)]
        public string? Notes { get; set; }
        
        public DateTime ActionDate { get; set; } = DateTime.UtcNow;
        
        // Navigation Properties
        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; } = null!;
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    }
}
