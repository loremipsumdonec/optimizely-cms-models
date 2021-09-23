using lorem_headless.Features.Web.Models;
using System.Web.Mvc;

namespace lorem_headless.Features.Web
{
    public class LoadWebModelActionFilter
        : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(filterContext.Result is ViewResult result) 
            {
                var webModel = new WebModel();
                webModel.AddExtension("content", result.ViewData.Model);

                result.ViewData.Model = webModel;
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}