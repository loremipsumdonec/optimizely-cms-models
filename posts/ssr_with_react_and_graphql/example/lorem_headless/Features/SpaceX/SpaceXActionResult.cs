using EPiServer.ServiceLocation;
using lorem_headless.Features.Render.Services;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace lorem_headless.Features.Render
{
    public class SpaceXActionResult
        : ActionResult
    {
        private readonly IClearScriptEngineManager _manager;
        
        public SpaceXActionResult() 
            : this(ServiceLocator.Current.GetInstance<IClearScriptEngineManager>())
           
        {
        }

        public SpaceXActionResult(
            IClearScriptEngineManager manager)
        {
            _manager = manager;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpContextBase contextBase = context.HttpContext;
            contextBase.Response.ContentType = "text/html";

            using (var session = _manager.GetRenderSessionForEngine("SpaceX"))
            using (var writer = new StreamWriter(context.HttpContext.Response.OutputStream, Encoding.UTF8))
            {
                string html = session.Render();
                writer.Write(html);
            }
        }
    }
}