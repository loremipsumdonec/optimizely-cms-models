using lorem_headless.Features.Breadcrumbs;
using lorem_headless.Features.Headless;
using lorem_headless.Features.Render.Filters;
using lorem_headless.Features.SpaceX;
using lorem_headless.Features.Web;
using System.Collections.Generic;
using System.Web.Mvc;

namespace lorem_headless.Features.Render.Services
{
    public class DefaultFilterProvider
        : IFilterProvider
    {
        public IEnumerable<Filter> GetFilters(
            ControllerContext controllerContext, 
            ActionDescriptor actionDescriptor)
        {
            var filters = new List<Filter>();

            if(controllerContext.Controller is IWebController) 
            {
                filters.Add(new Filter(new LoadWebModelActionFilter(), FilterScope.Global, 100));
                filters.Add(new Filter(new SpaceXActionFilter(), FilterScope.Global, 1));
                filters.Add(new Filter(new HeadlessActionFilter(), FilterScope.Global, 1));
                filters.Add(new Filter(new TransformModelToJson(), FilterScope.Global, 2));
            }

            return filters;
        }
    }
}