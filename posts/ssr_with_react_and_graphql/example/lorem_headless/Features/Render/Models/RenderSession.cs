using System;

namespace lorem_headless.Features.Render.Models
{
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
}
