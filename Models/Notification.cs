using System.ComponentModel.DataAnnotations;

namespace Mecmua.Models
{
    public enum NotificationType
    {
        CommentApproved,
        NewComment,
        ArticlePublished,
        ArticleRated,
        ArticleLiked,
        System
    }
    
    public class Notification : BaseEntity
    {
        public NotificationType Type { get; set; }
        
        [Required]
        public string Message { get; set; }
        
        public bool IsRead { get; set; } = false;
        public string? RedirectUrl { get; set; }
        
        // Foreign key
        public string UserId { get; set; }
        
        // Navigation property
        public virtual AppUser User { get; set; }
    }
} 