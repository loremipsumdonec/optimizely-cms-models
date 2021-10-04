---
type: "chapter"
book: "/optimizely/ssr-with-react-and-graphql"
chapter: "/part-2"

title: "Create a frontend project"
preamble: "We will start by setting up a very simple front-end project that uses a public GraphQL service. We will then use this project to get an server-side rendering."
---

The project will be built with [create-react-app](https://create-react-app.dev/) and use [Apollo Client](https://www.apollographql.com/docs/react/) to manage GraphQL. We will start with a simple component that retrieves data from https://api.spacex.land/graphql/.

> To get started with a project using React and Apollo client, follow this [guide](https://www.apollographql.com/docs/react/get-started/).

I have created a component called Missions that retrieves and prints the 10 latest mission names, below is a picture of what it looks like when rendered in the browser. You can find the front-end project [here](https://github.com/loremipsumdonec/optimizely-cms-models/tree/master/posts/ssr_with_react_and_graphql/example/spacex_react).

![](./resources/missons_component.png)

## Update the render method

For Apollo Client to work correctly on the server side, we will need to set some settings and also extract the state and save it in the HTML document so the application can rehydrate in the browser.

> You can find more information on how to enable server-side rendering for Apollo Client [here](https://www.apollographql.com/docs/react/performance/server-side-rendering/) 

```javascript
export const render = async (entrypoints) => {
	
	const applicationContent = await renderToStringWithData(React.createElement(App));
	const initialApolloClientState = client.extract();

	var files = [];

	const html = ` 
		<html>
			<head>
				${files.join('\n')}
			</head>
			<body>
				${applicationContent}
				<script>
					window.__APOLLO_STATE=${JSON.stringify(initialApolloClientState).replace(/</g, '\\u003c')}
				</script>
			</body>
		</html>`

    ....
}
```

## Communication

By default, when setting up Apollo Client it uses `fetch` . When rendering the application on the server we don't have access to `fetch`, so we need to customize Apollo Client´s data flow and we can do this with [links](https://www.apollographql.com/docs/react/api/link/introduction/).

With a new link and a custom service called `Connector` that we add with `AddHostObject` we can make it possible to send requests to the backend when server-side rendering.

```javascript
const directCommunicationWithBackend = new ApolloLink((operation, _) => {

	const { operationName, variables, query } = operation;
  
	let body = {
	  operationName,
	  variables,
	  query: print(query)
	}
  
	return new Observable(observer => {

		// eslint-disable-next-line no-undef
		Connector.Query('https://api.spacex.land/graphql', JSON.stringify(body))
			.then(response => { 
				operation.setContext({response});	

				var result = JSON.parse(response);
				
				observer.next(result);
				observer.complete();
				
				return result;
			});
		});
});
```

When working with links the graphql query will be in AST format and convert it to a string we need to use the [print](https://graphql.org/graphql-js/language/#print) function that comes with [graphql](graphql/language) package.

## Connector

The `Connector` is a service that we can use in the front-end project to send the rendered content to backend when it's done. We can also extend it with different methods, like the `Query` method thats used by the Apollo Client link `directCommunicationWithBackend`.

> You should avoid handling it as a global object, and instead strive to submit it as an input to the functions where it is needed.

## Render in backend

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

## Conclusion

This was a introduction that shows that it works to render the front-end application with _Microsoft ClearScript_ when it uses GraphQL to retrieve data from an external service. In the next chapter I will focus on adding support to GraphQL in an Optimizely CMS implementation.
