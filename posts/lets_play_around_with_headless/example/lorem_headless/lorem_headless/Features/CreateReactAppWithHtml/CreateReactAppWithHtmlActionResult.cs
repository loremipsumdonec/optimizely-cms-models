using EPiServer.ServiceLocation;
using lorem_headless.Features.Render.Services;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace lorem_headless.Features.CreateReactAppWithHtml
{
    public class CreateReactAppWithHtmlActionResult
        : ActionResult
    {
        private readonly IJavaScriptManager _manager;
        
        public CreateReactAppWithHtmlActionResult() 
            : this(ServiceLocator.Current.GetInstance<IJavaScriptManager>())
           
        {
        }

        public CreateReactAppWithHtmlActionResult(
            IJavaScriptManager manager)
        {
            _manager = manager;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpContextBase contextBase = context.HttpContext;
            contextBase.Response.ContentType = "text/html";

            using (var engine = _manager.GetEngine("create-react-app-with-html"))
            {
                string html = engine.CallFunction<string>("render");

                using (var writer = new StreamWriter(context.HttpContext.Response.OutputStream, Encoding.UTF8))
                {
                    writer.Write(html);
                }
            }
        }
    }
}