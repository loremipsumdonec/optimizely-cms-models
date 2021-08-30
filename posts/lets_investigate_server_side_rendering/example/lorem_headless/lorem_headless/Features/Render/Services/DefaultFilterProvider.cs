using lorem_headless.Features.CreateReactAppWithHtml;
using lorem_headless.Features.LoremHeadlessReact;
using lorem_headless.Features.Render.Filters;
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
                filters.Add(new Filter(new CreateReactAppActionFilter(), FilterScope.Global, 1));
                filters.Add(new Filter(new CreateReactAppWithHtmlActionFilter(), FilterScope.Global, 1));
                filters.Add(new Filter(new TransformModelToJson(), FilterScope.Global, 0));
            }

            return filters;
        }
    }
}