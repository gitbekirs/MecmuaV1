using System.ComponentModel.DataAnnotations;

namespace Mecmua.Models
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Slug { get; set; }
        
        // Navigation property
        public virtual ICollection<Article> Articles { get; set; }
        
        public Category()
        {
            Articles = new HashSet<Article>();
        }
    }
} 