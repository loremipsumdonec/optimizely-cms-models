using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using lorem_headless.Features.Headless.Models;
using lorem_headless.Features.Render;
using lorem_headless.Models.Pages;
using System.Web.Mvc;

namespace lorem_headless.Controllers
{
    [TemplateDescriptor(Inherited = true)]
    public class DefaultPageController
        : PageController<SitePage>, IWebController
    {
        [HttpGet]
        public ActionResult Index(SitePage currentPage)
        {
            ContextModel model = new ContextModel()
            {
                Url = Request.Url.LocalPath,
                PageId = currentPage.ContentLink.ID
            };

            if(currentPage is ArticlePage)
            {
                model.ModelType = nameof(ArticlePage);
            } 
            else if(currentPage is StartPage) 
            {
                model.ModelType = nameof(StartPage);
            }

            return View(model);
        }
    }
}