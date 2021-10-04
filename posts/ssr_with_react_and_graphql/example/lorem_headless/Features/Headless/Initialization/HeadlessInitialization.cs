using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using lorem_headless.Features.FirstIteration.Models;
using lorem_headless.Features.Headless.Models;
using lorem_headless.Features.Render.Models;
using lorem_headless.Features.Render.Services;
using Microsoft.ClearScript.V8;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace lorem_headless.Features.Headless.Initialization
{
    [InitializableModule]
    public class HeadlessInitialization
        : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<HeadlessQuery>();
            context.Services.AddSingleton<HeadlessSchema>();
        }

        public void Initialize(InitializationEngine context)
        {

            if (context.HostType == HostType.TestFramework)
            {
                return;
            }

            var entrypoints = $"[{string.Join(",", GetEntryPoints())}]";

            var manager = context.Locate.Advanced.GetInstance<IClearScriptEngineManager>();
            manager.Register("Headless", new ClearScriptEngineSettings()
            {
                ExecuteWhenRender = "render",
                MaxEngines = 2,
                ConnectorType = typeof(HeadlessConnector),
                Factory = () => {
                    var engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableTaskPromiseConversion);

                    string server = File.ReadAllText(
                        HttpContext.Current.Server.MapPath("~/Assets/SSRWithGraphQL/server/server.js"),
                        Encoding.UTF8
                    );

                    engine.Execute(server);
                    engine.Execute($"function render(modelAsJson) {{ lorem.render(JSON.parse(modelAsJson), {entrypoints}, Connector) }}");

                    return engine;
                }
            });
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        private IEnumerable<string> GetEntryPoints()
        {
            string content = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Assets/SSRWithGraphQL/asset-manifest.json"));
            var manifest = JsonConvert.DeserializeObject<JObject>(content);

            var entrypoints = manifest.SelectTokens("$.entrypoints[*]").Values<string>()
                .Select(v => "'" + "/Assets/SSRWithGraphQL/" + v + "'").ToList();

            return entrypoints;
        }
    }
}