using JavaScriptEngineSwitcher.V8;
using Lorem.Test.Framework.Optimizely.CMS.Utility;
using lorem_headless_tests.Extensions;
using lorem_headless_tests.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace lorem_headless_tests
{
    public class JavaScriptEngineTests
    {
        public JavaScriptEngineTests()
        {
            Resources = new DefaultResources();
        }

        public DefaultResources Resources { get; set; }

        private string SerializeWithCamelCase(object model) 
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }

        [Fact]
        public void CallFunction_WithNoInput_ReturnMessage()
        {
            string message = "hello from frontend!";
            string javascript = $"function shout() {{ return \"{message}\"}}";

            var engine = new V8JsEngine();
            engine.Execute(javascript);

            var actual = engine.CallFunction<string>("shout");

            Assert.Equal(message, actual);
        }

        [Fact]
        public void CallFunction_WithStringInput_ReturnInput()
        {
            string message = "hello from frontend";
            string javascript = "function giveBackInput(input) { return input;}";

            var engine = new V8JsEngine();
            engine.Execute(javascript);

            var actual = engine.CallFunction<string>("giveBackInput", message);

            Assert.Equal(message, actual);
        }

        [Fact]
        public void CallFunction_WithIntInputs_ReturnSum()
        {
            int first = 10;
            int second = 110;

            string javascript = "function getSum(first, second) { return first + second;}";

            var engine = new V8JsEngine();
            engine.Execute(javascript);

            var actual = engine.CallFunction<int>("getSum", first, second);

            Assert.Equal(first + second, actual);
        }
    
        [Fact]
        public async void CallRenderFunction_WithModel_ReturnHtml() 
        {
            var model = new
            {
                Heading = "Hello world",
                Preamble = IpsumGenerator.Generate(10, 18)
            };

            var modelAsJson = SerializeWithCamelCase(model);            

            var engine = new V8JsEngine();
            engine.Execute(
                Resources.GetContent("lets_render.js")
            );

            var html = engine.CallFunction<string>("render", modelAsJson);

            Assert.Equal(model.Heading, await html.GetTextAsync("h1"));
            Assert.Equal(model.Preamble, await html.GetTextAsync("p"));
        }
    }
}
