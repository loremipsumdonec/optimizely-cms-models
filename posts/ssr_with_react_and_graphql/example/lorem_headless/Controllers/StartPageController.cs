using EPiServer;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using lorem_headless.Features.Articles.Models;
using lorem_headless.Features.Render;
using lorem_headless.Models.Pages;
using System.Web.Mvc;

namespace lorem_headless.Controllers
{
    public class StartPageController
        : PageController<StartPage>, IWebController
    {
        private readonly IContentLoader _loader;
        private readonly IUrlResolver _resolver;

        public StartPageController(IContentLoader loader, IUrlResolver resolver)
        {
            _loader = loader;
            _resolver = resolver;
        }

        public ActionResult Index(StartPage currentPage) 
        {
            var model = new StartPageModel(currentPage)
            {
                Url = _resolver.GetUrl(currentPage)
            };

            LoadArticles(currentPage, model);

            return View(model);
        }

        private void LoadArticles(StartPage currentPage, StartPageModel model)
        {
            foreach(var child in _loader.GetChildren<ArticlePage>(currentPage.ContentLink))
            {
                model.Add(new Article() { 
                    Heading = child.Heading,
                    Preamble = child.Preamble,
                    Url = _resolver.GetUrl(child)
                });
            }
        }
    }
}