using Microsoft.ClearScript.V8;
using System;

namespace lorem_headless.Features.Render.Models
{
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
            ConnectorType = settings.ConnectorType;
        }

        public string ExecuteWhenRender { get; set; }

        public int MaxEngines { get; set; }

        public Func<V8ScriptEngine> Factory { get; set; }

        public Type ConnectorType { get; set; }
    }
}
