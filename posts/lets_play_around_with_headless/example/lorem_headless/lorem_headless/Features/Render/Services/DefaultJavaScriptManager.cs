using EPiServer.ServiceLocation;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;
using JSPool;
using System.Collections.Generic;

namespace lorem_headless.Features.Render.Services
{
    [ServiceConfiguration(typeof(IJavaScriptManager), Lifecycle = ServiceInstanceScope.Singleton)]
    public class DefaultJavaScriptManager
        : IJavaScriptManager
    {
        private Dictionary<string, JsPool> _pools = new Dictionary<string, JsPool>();

        public DefaultJavaScriptManager()
        {
            _pools = new Dictionary<string, JsPool>();

            JsEngineSwitcher.Current.DefaultEngineName = V8JsEngine.EngineName;
            JsEngineSwitcher.Current.EngineFactories.AddV8();
        }

        public IJsEngine GetEngine(string name)
        {
            return _pools[name].GetEngine();
        }

        public void Register(string name, JsPoolConfig config)
        {
            _pools.Add(name, new JsPool(config));
        }
    }
}