---
type: "chapter"
book: "/optimizely/lets-investigate-server-side-rendering"
chapter: "/part-1"

title: "Let's start with javascript"
preamble: "If we are going to use technologies like React or Vue, we need to know how to call a JavaScript function from .NET, and in this chapter, I'm going to show the basic of how we can do just that."
---

## Let's begin

Create a new project, it can be anything from a console, test to a web project, and then add the following nuget packages.

- [JavaScriptEngineSwitcher.V8](https://www.nuget.org/packages/JavaScriptEngineSwitcher.V8/)
- [Microsoft.ClearScript.V8.Native.win-x64](https://www.nuget.org/packages/Microsoft.ClearScript.V8.Native.win-x64/) or [Microsoft.ClearScript.V8.Native.win-x86](https://www.nuget.org/packages/Microsoft.ClearScript.V8.Native.win-x86/)

> All examples will be from test cases that I have set up in the example project [lorem_headless](https://github.com/loremipsumdonec/optimizely-cms-models/tree/master/posts/lets_investigate_server_side_rendering/example/lorem_headless) and from file [JavaScriptEngineTests.cs](https://github.com/loremipsumdonec/optimizely-cms-models/blob/master/posts/lets_play_around_with_headless/example/lorem_headless/lorem_headless_tests/JavaScriptEngineTests.cs)

If you choose to use _Microsoft.ClearScript.V8.Native.win-x64_ then you will need to change _Platform target_ from _Any CPU_ to _x64_ in the project properties. Unless you are using Visual Studio 2022 preview.

```text
JavaScriptEngineSwitcher.Core.JsEngineLoadException : Failed to create instance of the V8JsEngine. Most likely it happened, because the 'ClearScriptV8.win-x86.dll' assembly or one of its dependencies was not found. Try to install the Microsoft.ClearScript.V8.Native.win-x86 package via NuGet.
```

## A simple hello

Let's create a classic hello world JavaScript which we then call from .NET.

```csharp
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
```

It's also possible to send input to a function, such as a simple calculation of two values.

```csharp
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
```

## Sending something bigger

The views need more information than a few values, usually you have quite large and sometimes complex view models. JavaScript is dynamic language and does not need any classes that represent each view-model, so all we need to do is serialize the models to JSON and then deserialize them in the JavaScript.

```javascript
function render(modelAsJson) {
    const model = JSON.parse(modelAsJson);
    
    return `<html>
                <head>
                    <title>${model.heading}</title>
                </head>
                <body>
                    <h1>${model.heading}</h1>
                    <p>${model.preamble}</p>
                </body>
            </html>`;
}
```

```csharp
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
```

> When working with JavaScript, its more standard to use camelCase, while in .NET it is PascalCase . It can be much easier for the frontend team if you serialize the view-models in camelCase.

## Conclusion

These examples show the basis for how it works to call JavaScript from .NET and the principle for server-side rendering with, for example, React. 
