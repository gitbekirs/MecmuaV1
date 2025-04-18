using System;
using System.ComponentModel.DataAnnotations;

namespace Mecmua.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string? IPAddress { get; set; }
        public bool IsActive { get; set; } = true;
    }
} 