---
type: "chapter"
book: "/optimizely/ssr-with-react-and-graphql"
chapter: "/part-3"

title: "Render front-end project"
preamble: "We will start by setting up a very simple front-end project that uses a public GraphQL service. We will then use this project to get an server-side rendering."
---

If we build the front-end project _spacex_react_ with `npm run build:server` we can then take _server.js_ file and use it for server-side rendering. Below is an test case that renders the react application, you can find this in the file [CreateReactAppTests.cs](https://github.com/loremipsumdonec/optimizely-cms-models/blob/master/posts/ssr_with_react_and_graphql/example/lorem_headless_tests/CreateReactAppTests.cs)

```csharp
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
```

The test case uses _Microsoft.ClearScript.V8_ and starts an engine with the `V8ScriptEngineFlags.EnableTaskPromiseConversion` flag, which is needed to run the JavaScript. Here we also see that the Connector service is inserted into the JavaScript with `AddHostObject`.

## Connector

The `Connector` is a service that we can use in the front-end project to send the rendered content to backend when it's done. We can also extend it with different methods, like the `Query` method thats used by the Apollo Client link `directCommunicationWithBackend`.

> You should avoid handling it as a global object, and instead strive to submit it as an input to the functions where it is needed.

## Conclusion

This was a introduction that shows that it works to render the front-end application with Microsoft ClearScript when it uses GraphQL to retrieve data from an external service. In the next chapter I will focus on adding support to GraphQL in an Optimizely CMS implementation.
