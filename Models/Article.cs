using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Mecmua.Models
{
    public class Article : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Slug { get; set; }
        
        [StringLength(500)]
        public string? Summary { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        // Store multiple media URLs as JSON array
        public string? MediaUrlsJson { get; set; }
        
        [NotMapped]
        public List<string> MediaUrls
        {
            get => MediaUrlsJson == null ? new List<string>() : 
                JsonSerializer.Deserialize<List<string>>(MediaUrlsJson);
            set => MediaUrlsJson = JsonSerializer.Serialize(value);
        }
        
        public int ViewCount { get; set; } = 0;
        public bool IsFeatured { get; set; } = false;
        
        // Foreign keys
        public int CategoryId { get; set; }
        public string AuthorId { get; set; }
        
        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual AppUser Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
        
        public Article()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            ArticleTags = new HashSet<ArticleTag>();
        }
    }
} 