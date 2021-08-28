---
type: "chapter"
book: "/optimizely/lets-play-around-with-headless"
chapter: "/part-1"

title: "Let's start with javascript"
preamble: "If we are going to use technologies like React or Vue, we need to know how to call a JavaScript function from .NET, and in this chapter, I'm going to show the basic of how we can do just that."
---

## Let's begin

Create a new project, it can be anything from a console, test to a web project, and then add the following nuget packages.

- [JavaScriptEngineSwitcher.V8](https://www.nuget.org/packages/JavaScriptEngineSwitcher.V8/)
- [Microsoft.ClearScript.V8.Native.win-x64](https://www.nuget.org/packages/Microsoft.ClearScript.V8.Native.win-x64/) or [Microsoft.ClearScript.V8.Native.win-x86](https://www.nuget.org/packages/Microsoft.ClearScript.V8.Native.win-x86/)

> All examples will be from test cases that I have set up in the example project lorem_headless

If you choose to use _Microsoft.ClearScript.V8.Native.win-x64_ then you will need to change _Platform target_ from _Any CPU_ to _x64_ in the project properties. Unless you are using Visual Studio 2022 preview.

```text
JavaScriptEngineSwitcher.Core.JsEngineLoadException : Failed to create instance of the V8JsEngine. Most likely it happened, because the 'ClearScriptV8.win-x86.dll' assembly or one of its dependencies was not found. Try to install the Microsoft.ClearScript.V8.Native.win-x86 package via NuGet.
```

## Hello world

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

It is also possible to send input to a function that can then use it and return something else, such as a simple summation of two values.

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

These two examples show the basis for how it works to call JavaScript from .NET and the principle for achieving server-side rendering with, for example, React. You have defined some type of function which is then called with some input. The function will then return HTML which is forwarded to the client.
