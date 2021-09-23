using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using lorem_headless.Features.Render;
using lorem_headless.Models.Pages;
using System.Web.Mvc;

namespace lorem_headless.Features.LoremHeadlessReact
{
    public class CreateReactAppActionFilter
        : IActionFilter
    {
        private readonly IContentLoader _loader;

        public CreateReactAppActionFilter() 
            : this(ServiceLocator.Current.GetInstance<IContentLoader>())
        {
        }

        public CreateReactAppActionFilter(IContentLoader loader)
        {
            _loader = loader;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (CanTransform(filterContext))
            {
                filterContext.Result = new CreateReactAppActionResult();
            }
        }

        private StartPage GetStartPage() 
        {
            return _loader.Get<StartPage>(ContentReference.StartPage);
        }

        private bool CanTransform(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is ViewResult viewResult
                && viewResult.ViewData.Model != null
                && filterContext.HttpContext.Request.ContentType == "text/html")
            {
                var startPage = GetStartPage();
                return startPage?.Renderer == "create-react-app";
            }

            return false;
        }
    }
}