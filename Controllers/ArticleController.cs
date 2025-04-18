using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mecmua.Data.Repositories;
using Mecmua.Models;

namespace Mecmua.Controllers
{
    [AllowAnonymous]
    public class ArticleController : Controller
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Like> _likeRepository;
        private readonly IRepository<Tag> _tagRepository;
        
        public ArticleController(
            IRepository<Article> articleRepository,
            IRepository<Comment> commentRepository,
            IRepository<Like> likeRepository,
            IRepository<Tag> tagRepository)
        {
            _articleRepository = articleRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _tagRepository = tagRepository;
        }
        
        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return NotFound();
            }
            
            var articles = await _articleRepository.FindAsync(a => a.Slug == slug && a.IsActive);
            if (!articles.Any())
            {
                return NotFound();
            }
            
            var article = articles.First();
            
            // Increment view count
            article.ViewCount++;
            await _articleRepository.UpdateAsync(article);
            
            // Get approved comments
            var comments = await _commentRepository.FindAsync(
                c => c.ArticleId == article.Id && c.IsActive && c.IsApproved);
            
            ViewData["Comments"] = comments;
            
            // Get related articles from same category
            var relatedArticles = await _articleRepository.GetPagedAsync(
                1, 
                4,
                a => a.CategoryId == article.CategoryId && a.Id != article.Id && a.IsActive,
                q => q.OrderByDescending(a => a.CreatedAt),
                a => a.Author
            );
            
            ViewData["RelatedArticles"] = relatedArticles.Items;
            
            return View(article);
        }
        
        public async Task<IActionResult> Tag(string slug, int page = 1)
        {
            int pageSize = 10;
            
            if (string.IsNullOrEmpty(slug))
            {
                return NotFound();
            }
            
            var tags = await _tagRepository.FindAsync(t => t.Slug == slug && t.IsActive);
            if (!tags.Any())
            {
                return NotFound();
            }
            
            var tag = tags.First();
            
            // Get articles with this tag using ArticleTag
            var articleIds = tag.ArticleTags.Select(at => at.ArticleId);
            
            var result = await _articleRepository.GetPagedAsync(
                page,
                pageSize,
                a => a.IsActive && articleIds.Contains(a.Id),
                q => q.OrderByDescending(a => a.CreatedAt),
                a => a.Author,
                a => a.Category
            );
            
            ViewData["Tag"] = tag;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = result.TotalPages;
            
            return View("~/Views/Home/Index.cshtml", result.Items);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int articleId, string content, string guestName = null, int? rating = null)
        {
            // Validate article exists
            var article = await _articleRepository.GetByIdAsync(articleId);
            if (article == null || !article.IsActive)
            {
                return NotFound();
            }
            
            // Create comment
            var comment = new Comment
            {
                ArticleId = articleId,
                Content = content,
                Rating = rating,
                IsApproved = false, // Require approval by default
                IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
            };
            
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                comment.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            }
            else
            {
                if (string.IsNullOrEmpty(guestName))
                {
                    ModelState.AddModelError("", "Takma ad gereklidir.");
                    return RedirectToAction(nameof(Details), new { slug = article.Slug });
                }
                
                comment.GuestName = guestName;
            }
            
            await _commentRepository.AddAsync(comment);
            
            TempData["Message"] = "Yorumunuz gönderildi ve incelendikten sonra yayınlanacaktır.";
            
            return RedirectToAction(nameof(Details), new { slug = article.Slug });
        }
        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Like(int articleId)
        {
            // Validate article exists
            var article = await _articleRepository.GetByIdAsync(articleId);
            if (article == null || !article.IsActive)
            {
                return NotFound();
            }
            
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            // Check if already liked
            var existingLike = await _likeRepository.ExistsAsync(l => l.ArticleId == articleId && l.UserId == userId);
            if (existingLike)
            {
                // Remove like
                var likes = await _likeRepository.FindAsync(l => l.ArticleId == articleId && l.UserId == userId);
                await _likeRepository.DeleteAsync(likes.First());
                return Json(new { liked = false, count = article.Likes.Count - 1 });
            }
            else
            {
                // Add like
                var like = new Like
                {
                    ArticleId = articleId,
                    UserId = userId,
                    IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
                };
                
                await _likeRepository.AddAsync(like);
                return Json(new { liked = true, count = article.Likes.Count + 1 });
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Search(string q, int page = 1)
        {
            if (string.IsNullOrEmpty(q))
            {
                return RedirectToAction("Index", "Home");
            }
            
            int pageSize = 10;
            
            var searchTerms = q.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            var result = await _articleRepository.GetPagedAsync(
                page,
                pageSize,
                a => a.IsActive && (
                    searchTerms.Any(term => a.Title.Contains(term)) ||
                    searchTerms.Any(term => a.Content.Contains(term)) ||
                    searchTerms.Any(term => a.Summary.Contains(term))
                ),
                q => q.OrderByDescending(a => a.CreatedAt),
                a => a.Author,
                a => a.Category
            );
            
            ViewData["SearchQuery"] = q;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = result.TotalPages;
            
            return View("~/Views/Home/Index.cshtml", result.Items);
        }
    }
} 