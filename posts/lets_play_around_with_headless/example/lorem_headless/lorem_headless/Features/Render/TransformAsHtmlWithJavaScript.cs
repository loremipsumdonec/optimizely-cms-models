using EPiServer.ServiceLocation;
using lorem_headless.Features.Render.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace lorem_headless.Features.Render
{
    public class TransformAsHtmlWithJavaScript
        : ActionResult
    {
        private readonly string _name;
        private readonly string _functionName;
        private readonly IJavaScriptManager _manager;
        private readonly List<object> _arguments;

        public TransformAsHtmlWithJavaScript(string name,string functionName) 
            : this(
                  name, 
                  functionName, 
                  ServiceLocator.Current.GetInstance<IJavaScriptManager>()
                )
           
        {
        }

        public TransformAsHtmlWithJavaScript(
            string name, 
            string functionName, 
            IJavaScriptManager manager)
        {
            _name = name;
            _functionName = functionName;
            _manager = manager;
            _arguments = new List<object>();
        }

        public void Add(object argument) 
        {
            if(argument != null) 
            {
                _arguments.Add(argument);
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpContextBase contextBase = context.HttpContext;
            contextBase.Response.ContentType = "text/html";

            using (var engine = _manager.GetEngine(_name))
            {
                string html = engine.CallFunction<string>(
                    _functionName,
                    GetArguments()
                );

                using (StreamWriter writer = new StreamWriter(context.HttpContext.Response.OutputStream, Encoding.UTF8))
                {
                    writer.Write(html);
                }
            }
        }

        private object[] GetArguments()
        {
            if (_arguments.Count == 0)
            {
                return null;
            }

            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        ProcessExtensionDataNames = true
                    }
                }
            };

            return _arguments.Select(m => JsonConvert.SerializeObject(m, settings)).ToArray();
        }
    }
}