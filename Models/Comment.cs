using System.ComponentModel.DataAnnotations;

namespace Mecmua.Models
{
    public class Comment : BaseEntity
    {
        [Required]
        public string Content { get; set; }
        
        public int? Rating { get; set; }
        public bool IsApproved { get; set; } = false;
        
        // For guest comments
        [StringLength(50)]
        public string? GuestName { get; set; }
        
        // Foreign keys
        public int ArticleId { get; set; }
        public string? UserId { get; set; }  // Null for guest comments
        
        // Navigation properties
        public virtual Article Article { get; set; }
        public virtual AppUser User { get; set; }
    }
} 