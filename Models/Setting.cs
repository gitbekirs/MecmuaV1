using System.ComponentModel.DataAnnotations;

namespace Mecmua.Models
{
    public class Setting : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Key { get; set; }
        
        public string Value { get; set; }
        
        [StringLength(200)]
        public string? Description { get; set; }
    }
} 