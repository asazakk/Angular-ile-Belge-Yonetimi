using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_Ticaret.Domain.Entities
{
    public class Store : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        public int UserId { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        public virtual ICollection<PlatformIntegration> PlatformIntegrations { get; set; } = new List<PlatformIntegration>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
