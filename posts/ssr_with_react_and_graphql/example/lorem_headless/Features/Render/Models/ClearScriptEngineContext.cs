using lorem_headless.Features.Render.Services;
using Microsoft.ClearScript.V8;
using System;
using System.Collections.Concurrent;

namespace lorem_headless.Features.Render.Models
{
    public class ClearScriptEngineContext
        : ClearScriptEngineSettings
    {
        private readonly BlockingCollection<V8ScriptEngine> _engines;
        private object _door = new object();
        
        public ClearScriptEngineContext(ClearScriptEngineSettings settings)
            : base(settings)
        {
            _engines = new BlockingCollection<V8ScriptEngine>();

            if(MaxEngines == 0)
            {
                throw new ArgumentException("Need atlest one engine", nameof(MaxEngines));
            }

            CreateEngine();
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

        public IConnector CreateConnector()
        {
            return (IConnector)Activator.CreateInstance(ConnectorType);
        }

        public void DestroyEngine(V8ScriptEngine engine)
        {
            engine.Dispose();
        }
    }
}
