using System.ComponentModel.DataAnnotations;

namespace Mecmua.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "E-posta adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Takma ad gereklidir.")]
        [StringLength(50, ErrorMessage = "Takma ad en fazla 50 karakter olabilir.")]
        [Display(Name = "Takma Ad")]
        public string Nickname { get; set; }
        
        [Required(ErrorMessage = "Şifre gereklidir.")]
        [StringLength(100, ErrorMessage = "{0} en az {2} ve en fazla {1} karakter uzunluğunda olmalıdır.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Onayı")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
} 