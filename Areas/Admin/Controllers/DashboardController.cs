using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Mecmua.Data.Repositories;
using Mecmua.Models;
using System.Data;

namespace Mecmua.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<AppUser> _userRepository;
        
        public DashboardController(
            IRepository<Article> articleRepository,
            IRepository<Comment> commentRepository,
            IRepository<AppUser> userRepository)
        {
            _articleRepository = articleRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            // Get counts for dashboard
            var articleCount = await _articleRepository.CountAsync(a => a.IsActive);
            var pendingCommentCount = await _commentRepository.CountAsync(c => c.IsActive && !c.IsApproved);
            var userCount = await _userRepository.CountAsync(u => u.IsActive);
            
            ViewData["ArticleCount"] = articleCount;
            ViewData["PendingCommentCount"] = pendingCommentCount;
            ViewData["UserCount"] = userCount;
            
            // Get latest articles
            var latestArticles = await _articleRepository.GetPagedAsync(
                1,
                5,
                a => a.IsActive,
                q => q.OrderByDescending(a => a.CreatedAt),
                a => a.Author,
                a => a.Category
            );
            
            // Get pending comments
            var pendingComments = await _commentRepository.GetPagedAsync(
                1,
                5,
                c => c.IsActive && !c.IsApproved,
                q => q.OrderByDescending(c => c.CreatedAt),
                c => c.Article,
                c => c.User
            );
            
            ViewData["LatestArticles"] = latestArticles.Items;
            ViewData["PendingComments"] = pendingComments.Items;
            
            return View();
        }
    }
} 