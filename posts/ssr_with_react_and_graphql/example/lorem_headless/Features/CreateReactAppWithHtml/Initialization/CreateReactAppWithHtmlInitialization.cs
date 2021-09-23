using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using JSPool;
using lorem_headless.Features.Render.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace lorem_headless.Features.CreateReactAppWithHtml.Initialization
{
    [InitializableModule]
    public class CreateReactAppWithHtmlInitialization
        : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            if(context.HostType == HostType.TestFramework) 
            {
                return;
            }

            var entrypoints = $"[{string.Join(",", GetEntryPoints())}]";

            var manager = context.Locate.Advanced.GetInstance<IJavaScriptManager>();
            manager.Register("create-react-app-with-html", new JsPoolConfig()
            {   
                MaxEngines = 2,
                StartEngines = 0,
                Initializer = (e) => {

                    e.ExecuteFile(
                        HttpContext.Current.Server.MapPath("~/Assets/create-react-app-with-html/server/server.js"), Encoding.UTF8
                    );

                    e.Execute($"function render() {{ return lorem.render({entrypoints}) }}");
                }
            });
        }

        private IEnumerable<string> GetEntryPoints() 
        {
            string content = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Assets/create-react-app-with-html/asset-manifest.json"));
            var manifest = JsonConvert.DeserializeObject<JObject>(content);

            return manifest.SelectTokens("$.entrypoints[*]").Values<string>()
                .Select(v => "'" + "/Assets/create-react-app-with-html/" + v + "'");
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}