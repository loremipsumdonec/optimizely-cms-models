using EPiServer.ServiceLocation;
using lorem_headless.Features.Render.Models;
using System.Collections.Generic;

namespace lorem_headless.Features.Render.Services
{
    [ServiceConfiguration(ServiceType = typeof(IClearScriptEngineManager), 
        Lifecycle = ServiceInstanceScope.Singleton)]
    public class DefaultClearScriptEngineManager
        : IClearScriptEngineManager
    {
        private Dictionary<string, ClearScriptEngineContext> _settings;

        public DefaultClearScriptEngineManager()
        {
            _settings = new Dictionary<string, ClearScriptEngineContext>();
        }

        public void Register(string name, ClearScriptEngineSettings settings)
        {
            _settings.Add(name, new ClearScriptEngineContext(settings));
        }

        public RenderSession GetRenderSessionForEngine(string name)
        {
            var settings = _settings[name];
            return new RenderSession(settings);
        }
    }
}
