using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;
using lorem_headless_tests.Extensions;
using lorem_headless_tests.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private IEnumerable<string> GetEntryPoints(string name) 
        {
            string content = Resources.GetContent($"{name}/asset-manifest.json");
            var manifest = JsonConvert.DeserializeObject<JObject>(content);

            return manifest.SelectTokens("$.entrypoints")
                .Values<string>()
                .Where(s => s.EndsWith(".js"))
                .Select(s=> Path.Combine(Resources.Path, name, s));   
        }

        [Fact]
        public void LoadEntryPoints_ShouldThrowException()
        {
            var entrypoints = GetEntryPoints("CreateReactApp");
            var engine = new V8JsEngine();

            var exception = Assert.Throws<JsRuntimeException>(() =>
            {
                foreach (var file in entrypoints)
                {
                    engine.ExecuteFile(file);
                }
            });

            Assert.StartsWith("ReferenceError: document is not defined", exception.Message);
        }

        [Fact]
        public void LoadServer_ThrowsArgumentException() 
        {
            var engine = new V8JsEngine();
            string server = Resources.GetFile("CreateReactApp/server/server.js");

            engine.ExecuteFile(server);

            Assert.Throws<ArgumentException>(
                () => engine.CallFunction<string>("lorem.render")
            );
        }

        [Fact]
        public async void LoadServer_WithWrapper_RenderAppAndLinkHasExpectedText()
        {
            var engine = new V8JsEngine();
            string server = Resources.GetFile("CreateReactApp/server/server.js");

            engine.ExecuteFile(server);
            engine.Execute("function render() { return lorem.render(); }");

            var html = engine.CallFunction<string>("render");

            Assert.Equal("Learn React", await html.GetTextAsync("a"));
        }
    }
}
