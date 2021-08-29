using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace lorem_headless.Features.Render
{
    public class TransformAsJson
        : ActionResult
    {
        private readonly object _model;

        public TransformAsJson(object model)
        {
            _model = model;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpContextBase contextBase = context.HttpContext;
            contextBase.Response.ContentType = "application/json";

            JsonSerializer serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        ProcessExtensionDataNames = true
                    }
                }
            };

            using (TextWriter writer = new StreamWriter(context.HttpContext.Response.OutputStream))
            {
                serializer.Serialize(writer, _model);
            }
        }
    }
}