using lorem_headless.Features.Render.Models;
using lorem_headless.Features.Render.Services;
using lorem_headless_tests.Extensions;
using lorem_headless_tests.Services;
using Microsoft.ClearScript.V8;
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
        public async void RenderSpaceX_HasAListWithItems()
        {
            using (V8ScriptEngine engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableTaskPromiseConversion))
            {
                var connector = new Connector();
                engine.AddHostObject("Connector", connector);

                string server = Resources.GetContent("SpaceX/server/server.js");
                engine.Execute(server);
                engine.Execute("function render() { lorem.render([]); }");

                engine.Invoke("render");
                string html = connector.WaitForContent();

                Assert.False(string.IsNullOrEmpty(await html.GetTextAsync("li")));
            }
        }
    }
}
