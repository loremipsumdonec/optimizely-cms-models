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

namespace lorem_headless.Features.CreateReactAppFinal.Initialization
{
    [InitializableModule]
    public class CreateReactAppFinalInitialization
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
            manager.Register("create-react-app-final", new JsPoolConfig()
            {   
                MaxEngines = 2,
                StartEngines = 0,
                Initializer = (e) => {

                    e.ExecuteFile(
                        HttpContext.Current.Server.MapPath("~/Assets/create-react-app-final/server/server.js"), Encoding.UTF8
                    );

                    e.Execute($"function render(modelAsJson) {{ return lorem.render(JSON.parse(modelAsJson), {entrypoints}) }}");
                }
            });
        }

        private IEnumerable<string> GetEntryPoints() 
        {
            string content = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Assets/create-react-app-final/asset-manifest.json"));
            var manifest = JsonConvert.DeserializeObject<JObject>(content);

            var entrypoints = manifest.SelectTokens("$.entrypoints[*]").Values<string>()
                .Select(v => "'" + "/Assets/create-react-app-final/" + v + "'").ToList();

            return entrypoints;
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}