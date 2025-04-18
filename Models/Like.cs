namespace Mecmua.Models
{
    public class Like : BaseEntity
    {
        // Foreign keys
        public int ArticleId { get; set; }
        public string UserId { get; set; }
        
        // Navigation properties
        public virtual Article Article { get; set; }
        public virtual AppUser User { get; set; }
    }
} 