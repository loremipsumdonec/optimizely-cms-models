using System.Web.Mvc;

namespace lorem_headless.Features.Render.Filters
{
    public class TransformModelWithJavaScript
        : IActionFilter
    {
        private bool CanTransform(ActionExecutedContext filterContext) 
        {
            if (filterContext.Result is ViewResult viewResult
                && viewResult.ViewData.Model != null
                && filterContext.HttpContext.Request.ContentType == "text/html")
            {
                return true;
            }

            return false;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!CanTransform(filterContext))
            {
                return;
            }
        }
    }
}