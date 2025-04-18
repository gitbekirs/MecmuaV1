using System.ComponentModel.DataAnnotations;

namespace Mecmua.ViewModels
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Takma ad gereklidir.")]
        [StringLength(50, ErrorMessage = "Takma ad en fazla 50 karakter olabilir.")]
        [Display(Name = "Takma Ad")]
        public string Nickname { get; set; }
        
        [Url(ErrorMessage = "Geçerli bir URL giriniz.")]
        [Display(Name = "Profil Resmi URL")]
        public string? ProfilePictureUrl { get; set; }
        
        [StringLength(500, ErrorMessage = "Bio en fazla 500 karakter olabilir.")]
        [Display(Name = "Hakkımda")]
        public string? Bio { get; set; }
    }
} 