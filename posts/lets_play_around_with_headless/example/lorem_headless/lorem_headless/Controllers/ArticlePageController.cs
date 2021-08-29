using EPiServer.Web.Mvc;
using lorem_headless.Models.Pages;
using System.Web.Mvc;

namespace lorem_headless.Controllers
{
    public class ArticlePageController
        : PageController<ArticlePage>
    {
        public ActionResult Index(ArticlePage currentPage)
        {
            return View(new ArticlePageModel(currentPage));
        }
    }
}