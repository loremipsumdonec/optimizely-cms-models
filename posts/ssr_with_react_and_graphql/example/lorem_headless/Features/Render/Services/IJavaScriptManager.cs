using JavaScriptEngineSwitcher.Core;
using JSPool;

namespace lorem_headless.Features.Render.Services
{
    public interface IJavaScriptManager
    {
        void Register(string name, JsPoolConfig config);

        IJsEngine GetEngine(string name);
    }
}