using EPiServer.Web.Mvc;
using lorem_headless.Models.Pages;
using System.Web.Mvc;

namespace lorem_headless.Controllers
{
    public class StartPageController
        : PageController<StartPage>
    {
        public ActionResult Index(StartPage currentPage) 
        {
            return View(new StartPageModel(currentPage));
        }
    }
}