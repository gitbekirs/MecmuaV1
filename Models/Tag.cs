using System.ComponentModel.DataAnnotations;

namespace Mecmua.Models
{
    public class Tag : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Slug { get; set; }
        
        // Navigation property
        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
        
        public Tag()
        {
            ArticleTags = new HashSet<ArticleTag>();
        }
    }
} 