using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mecmua.Models;
using Mecmua.ViewModels;

namespace Mecmua.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        
        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var result = await _signInManager.PasswordSignInAsync(
                model.Email, 
                model.Password, 
                model.RememberMe, 
                lockoutOnFailure: true);
                
            if (result.Succeeded)
            {
                _logger.LogInformation("Kullanıcı giriş yaptı.");
                return RedirectToLocal(returnUrl);
            }
            
            if (result.IsLockedOut)
            {
                _logger.LogWarning("Kullanıcı hesabı kilitlendi.");
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
                return View(model);
            }
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            // Check if nickname is already taken
            var existingUser = await _userManager.FindByNameAsync(model.Nickname);
            if (existingUser != null)
            {
                ModelState.AddModelError(nameof(model.Nickname), "Bu takma ad zaten kullanılıyor.");
                return View(model);
            }
            
            var user = new AppUser 
            { 
                UserName = model.Email, 
                Email = model.Email,
                Nickname = model.Nickname,
                IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
            };
            
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                _logger.LogInformation("Kullanıcı yeni bir hesap oluşturdu.");
                
                // Add to Member role by default
                await _userManager.AddToRoleAsync(user, "Üye");
                
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Kullanıcı çıkış yaptı.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            
            var model = new EditProfileViewModel
            {
                Nickname = user.Nickname,
                Email = user.Email,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Bio = user.Bio
            };
            
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            
            // Check if nickname is changed and already taken
            if (model.Nickname != user.Nickname)
            {
                var existingUser = await _userManager.FindByNameAsync(model.Nickname);
                if (existingUser != null)
                {
                    ModelState.AddModelError(nameof(model.Nickname), "Bu takma ad zaten kullanılıyor.");
                    return View(model);
                }
                
                user.Nickname = model.Nickname;
            }
            
            // Check if email is changed
            if (model.Email != user.Email)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(nameof(model.Email), "Bu e-posta adresi zaten kullanılıyor.");
                    return View(model);
                }
                
                user.Email = model.Email;
                user.UserName = model.Email; // Username is same as email
            }
            
            user.ProfilePictureUrl = model.ProfilePictureUrl;
            user.Bio = model.Bio;
            user.UpdatedAt = DateTime.Now;
            
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            
            TempData["Message"] = "Profil bilgileriniz güncellendi.";
            return RedirectToAction(nameof(Profile));
        }
        
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            
            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("Kullanıcı şifresini başarıyla değiştirdi.");
            
            TempData["Message"] = "Şifreniz başarıyla değiştirildi.";
            return RedirectToAction(nameof(Profile));
        }
        
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
} 