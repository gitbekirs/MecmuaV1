using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mecmua.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string Nickname { get; set; }
        
        public string? ProfilePictureUrl { get; set; }
        
        [StringLength(500)]
        public string? Bio { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string? IPAddress { get; set; }
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        
        public AppUser()
        {
            Articles = new HashSet<Article>();
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            Notifications = new HashSet<Notification>();
        }
    }
} 