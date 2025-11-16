using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Danistay.Domain.Entities
{
    public class StockHistory : BaseEntity
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int PreviousQuantity { get; set; }

        [Required]
        public int NewQuantity { get; set; }

        [Required]
        public int ChangeQuantity { get; set; }

        [Required]
        [StringLength(50)]
        public string ChangeType { get; set; } = string.Empty; // Sale, Restock, Adjustment, Return

        [StringLength(500)]
        public string? Reason { get; set; }

        public int? OrderId { get; set; }

        // Navigation Properties
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;
    }
}
