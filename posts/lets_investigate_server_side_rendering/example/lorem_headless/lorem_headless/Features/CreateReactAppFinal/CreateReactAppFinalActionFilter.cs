using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using lorem_headless.Models.Pages;
using System.Web.Mvc;

namespace lorem_headless.Features.CreateReactAppFinal
{
    public class CreateReactAppFinalActionFilter
        : IActionFilter
    {
        private readonly IContentLoader _loader;

        public CreateReactAppFinalActionFilter() 
            : this(ServiceLocator.Current.GetInstance<IContentLoader>())
        {
        }

        public CreateReactAppFinalActionFilter(IContentLoader loader)
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
                var result = (ViewResult)filterContext.Result;
                filterContext.Result = new CreateReactAppFinalActionResult(result.ViewData.Model);
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
                return startPage?.Renderer == "create-react-app-final";
            }

            return false;
        }
    }
}