using JavaScriptEngineSwitcher.V8;
using Xunit;

namespace lorem_headless_tests
{
    public class JavaScriptEngineTests
    {
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
    }
}
