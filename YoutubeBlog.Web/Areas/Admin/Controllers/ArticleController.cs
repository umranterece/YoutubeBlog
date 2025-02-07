using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YoutubBlog.Entity.DTOs.Articles;
using YoutubeBlog.Service.Services.Abstractions;
using YoutubeBlog.Service.Services.Concrete;

namespace YoutubeBlog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }
        public async Task<IActionResult> Index()
        {
            var artisles = await articleService.GetAllArticleWithCategoryNonDeletedAsync();
            return View(artisles);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ArticleDto articleDto)
        {
            return View();
        }
    }
}
