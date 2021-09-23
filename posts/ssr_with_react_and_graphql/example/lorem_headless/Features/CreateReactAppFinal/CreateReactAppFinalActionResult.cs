using EPiServer.ServiceLocation;
using lorem_headless.Features.Render.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace lorem_headless.Features.CreateReactAppFinal
{
    public class CreateReactAppFinalActionResult
        : ActionResult
    {
        private readonly IJavaScriptManager _manager;
        private readonly object _model;
        
        public CreateReactAppFinalActionResult(object model) 
            : this(model, ServiceLocator.Current.GetInstance<IJavaScriptManager>())
           
        {
        }

        public CreateReactAppFinalActionResult(
            object model,
            IJavaScriptManager manager)
        {
            _model = model;
            _manager = manager;
        }

        private string SerializeWithCamelCase(object model)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpContextBase contextBase = context.HttpContext;
            contextBase.Response.ContentType = "text/html";

            using (var engine = _manager.GetEngine("create-react-app-final"))
            {
                string modelAsJson = SerializeWithCamelCase(_model);
                string html = engine.CallFunction<string>("render", modelAsJson);

                using (var writer = new StreamWriter(context.HttpContext.Response.OutputStream, Encoding.UTF8))
                {
                    writer.Write(html);
                }
            }
        }
    }
}