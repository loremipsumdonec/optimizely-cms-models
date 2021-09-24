using lorem_headless.Features.Render.Models;

namespace lorem_headless.Features.Render.Services
{
    public interface IClearScriptEngineManager 
    {
        void Register(string name, ClearScriptEngineSettings settings);

        RenderSession GetRenderSessionForEngine(string name);
    }
}
