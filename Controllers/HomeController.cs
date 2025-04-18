using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mecmua.Data.Repositories;
using Mecmua.Models;
using System.Diagnostics;

namespace Mecmua.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Category> _categoryRepository;

        public HomeController(
            ILogger<HomeController> logger,
            IRepository<Article> articleRepository,
            IRepository<Category> categoryRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            
            var result = await _articleRepository.GetPagedAsync(
                page,
                pageSize,
                a => a.IsActive,
                q => q.OrderByDescending(a => a.CreatedAt),
                a => a.Author,
                a => a.Category
            );
            
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = result.TotalPages;
            
            return View(result.Items);
        }
        
        public async Task<IActionResult> Category(string slug, int page = 1)
        {
            int pageSize = 10;
            
            var category = await _categoryRepository.FindAsync(c => c.Slug == slug && c.IsActive);
            if (!category.Any())
            {
                return NotFound();
            }
            
            var categoryId = category.First().Id;
            
            var result = await _articleRepository.GetPagedAsync(
                page,
                pageSize,
                a => a.IsActive && a.CategoryId == categoryId,
                q => q.OrderByDescending(a => a.CreatedAt),
                a => a.Author,
                a => a.Category
            );
            
            ViewData["Category"] = category.First();
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = result.TotalPages;
            
            return View("Index", result.Items);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
