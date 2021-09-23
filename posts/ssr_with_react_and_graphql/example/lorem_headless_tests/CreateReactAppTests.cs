using lorem_headless.Features.Render.Services;
using lorem_headless_tests.Extensions;
using lorem_headless_tests.Services;
using Microsoft.ClearScript.V8;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Xunit;

namespace lorem_headless_tests
{
    public class CreateReactAppTests
    {
        public CreateReactAppTests()
        {
            Resources = new DefaultResources();
        }

        public DefaultResources Resources { get; set; }

        [Fact]
        public void Donec()
        {
            var manager = new ClearScriptEngineManager();
            manager.Register("lorem_donec", new ClearScriptEngineSettings()
            {
                ExecuteWhenRender = "render",
                MaxEngines = 2,
                ConnectorType = typeof(Connector),
                Factory = () => {
                    var engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableTaskPromiseConversion);

                    string server = Resources.GetContent("CreateReactAppWithGraphQL/server/server.js");
                    engine.Execute(server);
                    engine.Execute("function render() { lorem.render() }");

                    return engine;
                }
            });

            using(var session = manager.GetRenderSessionForEngine("lorem_donec"))
            {
                var html = session.Render();
            }

            using (var session = manager.GetRenderSessionForEngine("lorem_donec"))
            {
                var html = session.Render();
            }

            using (var session = manager.GetRenderSessionForEngine("lorem_donec"))
            {
                var html = session.Render();
            }

            using (var session = manager.GetRenderSessionForEngine("lorem_donec"))
            {
                var html = session.Render();
            }
        }

        [Fact]
        public async void Lorem()
        {
            using (V8ScriptEngine engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableTaskPromiseConversion))
            {
                var connector = new Connector();
                engine.AddHostObject("Connector", connector);

                string server = Resources.GetContent("CreateReactAppWithGraphQL/server/server.js");
                engine.Execute(server);

                engine.Invoke("render");
                string html = connector.WaitForContent();

                Assert.False(string.IsNullOrEmpty(await html.GetTextAsync("li")));
            }
        }
    }

    public class ClearScriptEngineSettings
    {
        public ClearScriptEngineSettings()
        {
        }

        public ClearScriptEngineSettings(ClearScriptEngineSettings settings)
        {
            ExecuteWhenRender = settings.ExecuteWhenRender;
            MaxEngines = settings.MaxEngines;
            Factory = settings.Factory;
        }

        public string ExecuteWhenRender { get; set; }

        public int MaxEngines { get; set; }

        public Func<V8ScriptEngine> Factory { get; set; }

        public Type ConnectorType { get; set; }
    }

    public class RenderSession
        : IDisposable
    {
        private readonly ClearScriptEngineContext _context;

        public RenderSession(ClearScriptEngineContext context)
        {
            _context = context;
        }

        public string Render(params object[] args)
        {
            var engine = _context.Take();

            try
            {
                var connector = _context.CreateConnector();
                engine.AddHostObject("Connector", connector);

                engine.Invoke(_context.ExecuteWhenRender, args);
                return connector.WaitForContent();
            }
            finally
            {
                _context.Return(engine);
            }
        }

        public void Dispose()
        {
        }
    }

    public class ClearScriptEngineContext
        : ClearScriptEngineSettings
    {
        private readonly BlockingCollection<V8ScriptEngine> _engines;
        private object _door = new object();
        
        public ClearScriptEngineContext(ClearScriptEngineSettings settings)
            : base(settings)
        {
            _engines = new BlockingCollection<V8ScriptEngine>();
        }

        public int ActiveEngines { get; private set; }

        public V8ScriptEngine Take()
        {
            if (!_engines.TryTake(out V8ScriptEngine engine))
            {
                if (ActiveEngines < MaxEngines)
                {
                    CreateEngine();
                }

                if (!_engines.TryTake(out engine, 500))
                {
                    throw new TimeoutException($"Could not get any engine within {500}ms");
                }
            }

            return engine;
        }

        public void Return(V8ScriptEngine engine)
        {
            _engines.Add(engine);
        }

        public void CreateEngine()
        {
            lock(_door)
            {
                _engines.Add(Factory.Invoke());
                ActiveEngines++;
            }
        }

        public Connector CreateConnector()
        {
            return (Connector)Activator.CreateInstance(ConnectorType);
        }

        public void DestroyEngine(V8ScriptEngine engine)
        {
            engine.Dispose();
        }
    }

    public class ClearScriptEngineManager
    {
        private readonly Dictionary<string, ClearScriptEngineContext> _settings;

        public ClearScriptEngineManager()
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
