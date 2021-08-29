using System.Web.Mvc;

namespace lorem_headless.Features.Render.Filters
{
    public class TransformModelToJson
        : IActionFilter
    {
        private bool CanTransform(ActionExecutedContext filterContext)
        {
            if(filterContext.Result is ViewResult viewResult 
                && viewResult.ViewData.Model != null
                && filterContext.HttpContext.Request.ContentType == "application/json") 
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

            var result = (ViewResult)filterContext.Result;

            filterContext.Result = new TransformAsJson(result.ViewData.Model);
        }
    }
}