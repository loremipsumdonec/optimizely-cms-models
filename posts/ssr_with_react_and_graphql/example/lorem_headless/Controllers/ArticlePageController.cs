using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using lorem_headless.Features.Render;
using lorem_headless.Models.Pages;
using System.Web.Mvc;

namespace lorem_headless.Controllers
{
    public class ArticlePageController
        : PageController<ArticlePage>, IWebController
    {
        private readonly IUrlResolver _resolver;

        public ArticlePageController(IUrlResolver resolver)
        {
            _resolver = resolver;
        }

        public ActionResult Index(ArticlePage currentPage)
        {
            var model = new ArticlePageModel(currentPage)
            {
                Url = _resolver.GetUrl(currentPage)
            };

            return View(model);
        }
    }
}