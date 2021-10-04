using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using lorem_headless.Features.Headless.Models;
using lorem_headless.Features.Headless.Services;
using lorem_headless.Features.Render;
using lorem_headless.Models.Pages;
using System.Web.Mvc;

namespace lorem_headless.Controllers
{
    [TemplateDescriptor(Inherited = true)]
    public class DefaultPageController
        : PageController<SitePage>, IWebController
    {
        private readonly ContextModelService _service;

        public DefaultPageController(ContextModelService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Index(SitePage _)
        {
            return View(_service.GetContextModel(
                Request.Url.LocalPath)
            );
        }
    }
}