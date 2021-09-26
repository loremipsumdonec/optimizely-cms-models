using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using lorem_headless.Features.Render.Models;
using lorem_headless.Features.Render.Services;
using Microsoft.ClearScript.V8;
using System.Text;
using System.Web;

namespace lorem_headless.Features.SpaceX.Initialization
{
    [InitializableModule]
    public class SpaceXInitialization
        : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            if(context.HostType == HostType.TestFramework) 
            {
                return;
            }

            var manager = context.Locate.Advanced.GetInstance<IClearScriptEngineManager>();
            manager.Register("SpaceX", new ClearScriptEngineSettings()
            {
                ExecuteWhenRender = "render",
                MaxEngines = 2,
                ConnectorType = typeof(Connector),
                Factory = () => {
                    var engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableTaskPromiseConversion);

                    string server = System.IO.File.ReadAllText(
                        HttpContext.Current.Server.MapPath("~/Assets/SpaceX/server/server.js"),
                        Encoding.UTF8
                    );

                    engine.Execute(server);
                    engine.Execute("function render() { lorem.render([]) }");

                    return engine;
                }
            });
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}