using lorem_headless.Features.Render.Filters;
using System.Collections.Generic;
using System.Web.Mvc;

namespace lorem_headless.Features.Render.Services
{
    public class RenderFilterProvider
        : IFilterProvider
    {
        public IEnumerable<Filter> GetFilters(
            ControllerContext controllerContext, 
            ActionDescriptor actionDescriptor)
        {
            var filters = new List<Filter>();


            filters.Add(new Filter(new TransformModelToJson(), FilterScope.Global, 0));

            filters.Add(new Filter(new TransformModelWithJavaScript(), FilterScope.Global, 1));

            return filters;
        }
    }
}