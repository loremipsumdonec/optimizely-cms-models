using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using lorem_headless.Features.Render.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace lorem_headless.Features.Headless
{
    public class HeadlessActionResult
        : ActionResult
    {
        private readonly IClearScriptEngineManager _manager;
        private readonly object _model;
        
        public HeadlessActionResult(object model) 
            : this(model, ServiceLocator.Current.GetInstance<IClearScriptEngineManager>())
           
        {
        }

        public HeadlessActionResult(
            object model,
            IClearScriptEngineManager manager)
        {
            _model = model;
            _manager = manager;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpContextBase contextBase = context.HttpContext;
            contextBase.Response.ContentType = "text/html";

            using (var session = _manager.GetRenderSessionForEngine("Headless"))
            using (var writer = new StreamWriter(context.HttpContext.Response.OutputStream, Encoding.UTF8))
            {
                var settings = new JsonSerializerSettings
                {
                    Culture = ContentLanguage.PreferredCulture,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy
                        {
                            ProcessExtensionDataNames = true
                        }
                    }
                };

                string modelAsJson = JsonConvert.SerializeObject(_model, settings);
                string html = session.Render(modelAsJson);
                writer.Write(html);
            }
        }
    }
}