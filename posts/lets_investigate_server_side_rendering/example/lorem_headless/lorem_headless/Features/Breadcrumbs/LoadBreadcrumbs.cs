using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using lorem_headless.Features.Breadcrumbs.Services;
using lorem_headless.Features.Web.Models;
using System.Web.Mvc;

namespace lorem_headless.Features.Breadcrumbs
{
    public class LoadBreadcrumbs
        : IActionFilter
    {
        private readonly BreadcrumbsService _service;

        public LoadBreadcrumbs()
            : this(ServiceLocator.Current.GetInstance<BreadcrumbsService>())
        {
        }

        public LoadBreadcrumbs(BreadcrumbsService service)
        {
            _service = service;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is ViewResult result 
                && result.ViewData.Model is WebModel model)
            {
                var breadcrumbs = _service.GetBreadcrumbs(GetCurrentPage(filterContext));
                model.AddExtension("breadcrumbs", breadcrumbs);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        private PageData GetCurrentPage(ActionExecutedContext filterContext) 
        {
            return filterContext.HttpContext.Request?.RequestContext?.GetRoutedData<PageData>();
        }
    }
}