using Lorem.Test.Framework.Optimizely.CMS.Utility;
using lorem_headless_tests.Extensions;
using lorem_headless_tests.Services;
using Microsoft.ClearScript.V8;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading;
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

            var engine = new V8ScriptEngine();
            engine.Execute(javascript);

            var actual = engine.Invoke("shout");

            Assert.Equal(message, actual);
        }

        [Fact]
        public void CallFunction_WithStringInput_ReturnInput()
        {
            string message = "hello from frontend";
            string javascript = "function giveBackInput(input) { return input;}";

            var engine = new V8ScriptEngine();
            engine.Execute(javascript);

            var actual = engine.Invoke("giveBackInput", message);

            Assert.Equal(message, actual);
        }

        [Fact]
        public void CallFunction_WithIntInputs_ReturnSum()
        {
            int first = 10;
            int second = 110;

            string javascript = "function getSum(first, second) { return first + second;}";

            var engine = new V8ScriptEngine();
            engine.Execute(javascript);

            var actual = engine.Invoke("getSum", first, second);

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

            var engine = new V8ScriptEngine();
            engine.Execute(
                Resources.GetContent("lets_render.js")
            );

            var html = (string)engine.Invoke("render", modelAsJson);

            Assert.Equal(model.Heading, await html.GetTextAsync("h1"));
            Assert.Equal(model.Preamble, await html.GetTextAsync("p"));
        }
    
        [Fact]
        public void GetValues_UsingPromise_ShouldOnlyGetTwoOfThreeValues() 
        {
            var engine = new V8ScriptEngine();

            engine.Execute(
                @"function loadThree() {
                    return new Promise(function(resolve, reject) {

                        for (let step = 0; step < 500; step++) {
                            //do some work
                        }

                        resolve(3);
                    });
                }

                function getValues() {
                    let values = [1,2];
                    loadThree().then(three => values.push(three));

                    return values.join(',');
                }"
            );

            var values = engine.Invoke("getValues");

            Assert.Equal("1,2", values);
        }

        [Fact]
        public void GetValues_WhenUsingWait_ShouldGetAllThreeValues()
        {
            var set = new ManualResetEventSlim();

            var engine = new V8ScriptEngine();
            engine.AddHostObject("set", set);

            engine.Execute(
                @"
                let output = null;

                function loadThree() {
                    return new Promise(function(resolve, reject) {

                        for (let step = 0; step < 500; step++) {
                            //do some work
                        }

                        resolve(3);
                    });
                }

                async function getValues() {
                    let values = [1,2];
                    let three = await loadThree();
                    values.push(three);

                    output = values.join(',');
                    set.Set();
                }"
            );

            engine.Invoke("getValues");
            set.Wait(2000);

            var values = engine.Evaluate("output");

            Assert.Equal("1,2,3", values);
        }
    }
}
