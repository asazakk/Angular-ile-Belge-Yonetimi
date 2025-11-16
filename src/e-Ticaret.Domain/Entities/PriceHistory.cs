using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_Ticaret.Domain.Entities
{
    public class PriceHistory : BaseEntity
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PreviousPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal NewPrice { get; set; }

        [Required]
        [StringLength(50)]
        public string ChangeType { get; set; } = string.Empty; // Manual, Automatic, Campaign, Competitive

        [StringLength(500)]
        public string? Reason { get; set; }

        public int? ChangedByUserId { get; set; }

        // Navigation Properties
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;
    }
}
