---
type: "chapter"
book: "/optimizely/ssr_with_react_and_graphql"
chapter: "/part-1"

title: "Let's start with javascript"
preamble: "Before we can start implementing support for GraphQL, I intend to introduce some features that we will use in this implementation."
---

One of the differences from before is that we will now use [Microsoft ClearScript](https://github.com/microsoft/ClearScript) directly. This is because we need more control and can then not go via [JavaScriptEngineSwitcher](https://github.com/Taritsyn/JavaScriptEngineSwitcher). 

There is no major difference in the syntax, and below is an example where we use `V8ScriptEngine`.

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

> If you look through all the test cases in the file XYK and compare these with the test cases from the previous post, you can see there is not major difference.

### Support for asynchronous functions

But since we will start using GraphQL, this will make the React application use asynchronous functions. We did not allow any async calls in the previous implementation, one of the reasons for this is to keep it _simple_. But now we have to support async calls.

If we start with the code below and run it, we will only get the result `1,2` from the function. The value 3 comes later. But the application has already called and retrieved the value when it happens.

```csharp
public void FirstCaseWithPromise() 
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

What is needed is that we need to wait until everything is finished. So, the javascript must send back a signal when it is finished, and only then can we retrieve the value.

This is a common problem and nothing that should be new. Especially if you have been working with multi-threads, and .NET has several tools for this.

### AddHostObject

By using the `AddHostObject` method, we can make .NET objects available in the javascript. Below is an example of when an instance of `ManualResetEventSlim` is inserted via AddHostObject which can then be called.

```csharp
public void SolveCaseWithPromise()
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

With the help of `AddHostObject`, it is now possible for us to wait for the javascript to be completed when using async functions. You can find theses tests in the file XYK.

## Conclusion

This is the basic principle of how to handle the problem with async function, then you can do musch more things with `AddHostObject`.
