using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using JSPool;
using lorem_headless.Features.Render.Services;
using System.Text;
using System.Web;

namespace lorem_headless.Features.CreateReactApp.Initialization
{
    [InitializableModule]
    public class CreateReactAppInitialization
        : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            if(context.HostType == HostType.TestFramework) 
            {
                return;
            }

            var manager = context.Locate.Advanced.GetInstance<IJavaScriptManager>();
            manager.Register("create-react-app", new JsPoolConfig()
            {   
                MaxEngines = 2,
                StartEngines = 0,
                Initializer = (e) => {

                    e.ExecuteFile(
                        HttpContext.Current.Server.MapPath("~/Assets/create-react-app/server/server.js"), Encoding.UTF8
                    );

                    e.Execute("function render() { return lorem.render() }");
                }
            });
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}