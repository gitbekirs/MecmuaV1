namespace Mecmua.Models
{
    public class ArticleTag
    {
        public int ArticleId { get; set; }
        public int TagId { get; set; }
        
        // Navigation properties
        public virtual Article Article { get; set; }
        public virtual Tag Tag { get; set; }
    }
} 