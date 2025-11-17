using System.ComponentModel.DataAnnotations;

namespace e_Ticaret.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? Department { get; set; }
        
        [StringLength(100)]
        public string? Position { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime? LastLoginDate { get; set; }
        
        // Navigation Properties
        public virtual ICollection<Document> CreatedDocuments { get; set; } = new List<Document>();
        public virtual ICollection<Document> UpdatedDocuments { get; set; } = new List<Document>();
        public virtual ICollection<DocumentAction> DocumentActions { get; set; } = new List<DocumentAction>();
    }
}
