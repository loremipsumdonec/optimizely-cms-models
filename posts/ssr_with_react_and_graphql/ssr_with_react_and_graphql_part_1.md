---
type: "chapter"
book: "/optimizely/ssr-with-react-and-graphql"
chapter: "/part-1"

title: "Render JavaScript that use promises"
preamble: "Before we can start implementing support for GraphQL, I intend to introduce some features that we will use in this implementation."
---

One of the differences from before is that we will now use [Microsoft ClearScript](https://github.com/microsoft/ClearScript) directly. There is no major difference in the syntax compared to [JavaScriptEngineSwitcher](https://github.com/Taritsyn/JavaScriptEngineSwitcher)., and below is an example where we use `V8ScriptEngine`.

```csharp
public void CallFunction_WithNoInput_ReturnMessage()
{
    string message = "hello from frontend!";
    string javascript = $"function shout() {{ return \"{message}\"}}";

    var engine = new V8ScriptEngine();
    engine.Execute(javascript);

    var actual = engine.Invoke("shout");

    Assert.Equal(message, actual);
}
```

> If you look through all the test cases in the file [JavaScriptEngineTests.cs](https://github.com/loremipsumdonec/optimizely-cms-models/blob/master/posts/ssr_with_react_and_graphql/example/lorem_headless_tests/JavaScriptEngineTests.cs) and compare these with the test cases from the [previous post](https://github.com/loremipsumdonec/optimizely-cms-models/blob/master/posts/lets_investigate_server_side_rendering/example/lorem_headless/lorem_headless_tests/JavaScriptEngineTests.cs), you can see there is not major difference.

### Support for promises

But since we will start using [GraphQL](https://graphql.org/), this will make the React application use promises. We did not allow these in the previous implementation, one of the reasons for this is to keep it _simple_.

If we start with the code below and run it, we will only get the result `1,2` from the function. The value 3 comes later. But the application has already called and retrieved the value when it happens.

```csharp
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
```

What is needed is that we need to wait until everything is finished. So, the JavaScript must send back a signal when it is finished, and only then can we retrieve the value.

### AddHostObject

By using the `AddHostObject` method, we can make .NET objects available in the JavaScript. Below is an example of when an instance of `ManualResetEventSlim` is inserted via `AddHostObject` which can then be called.

```csharp
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
```

With the help of `AddHostObject`, it is now possible for us to wait for the JavaScript to be completed when using a Promise. 

> You can find theses tests in the file [JavaScriptEngineTests.cs](https://github.com/loremipsumdonec/optimizely-cms-models/blob/master/posts/ssr_with_react_and_graphql/example/lorem_headless_tests/JavaScriptEngineTests.cs).

## Conclusion

This is the basic of how to handle promises in JavaScript, and with `AddHostObject` you can introduce other type of functions.
