---
type: "chapter"
book: "/optimizely/ssr-with-react-and-graphql"
chapter: "/part-2"

title: "Create a frontend project"
preamble: "We will start by setting up a very simple front-end project that uses a public GraphQL service. We will then use this project to get an server-side rendering."
---

The project will be built with the create-react app and use Apollo client to manage GraphQL. The project will have a simple component that retrieves data from https://api.spacex.land/graphql/ with the following query.

```graphql
query GetLaunchesPast($limit:Int!)
{
  launchesPast(limit: $limit) {
    mission_name
  }
}
```

> To get started with a project using React and Apollo client, follow this [guide](https://www.apollographql.com/docs/react/get-started/).

## Two entry points to the application

We will need two entry points to the application, one for the browser and the other for the server. For Apollo Client to work correctly on the server side, we will need to have some other settings and also extract the state and save the state in the html document so the application can hydrate correct in the browser.

> You can find more information on how to enable server-side rendering for Apollo Client [here](https://www.apollographql.com/docs/react/performance/server-side-rendering/) 

```javascript
export const render = async (entrypoints) => {
	
	const applicationContent = await renderToStringWithData(React.createElement(App));
	const initialApolloClientState = client.extract();

	var files = [];

	if(entrypoints) {
		for (const entrypoint of entrypoints) {
			if(entrypoint.endsWith('.css')) {
				files.push(`<link rel="stylesheet" href="${entrypoint}">`)
				continue;
			}
	
			files.push(`<script src="${entrypoint}"></script>`)
		}
	}

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

	// eslint-disable-next-line no-undef
	Connector.Send(html);
}
```

## Communication

We will also need to change a bit how Apollo Client calls the GraphQL services. By default, when setting up Apollo Client in the browser, you use the createHttpLink function. Which creates an `HttpLink` which in turn uses `fetch`.

But when we are going to render the application on the server side, we need to change how this works, mainly because fetch is not available.

## Conclusion

This is the basic principle of how to handle the problem with async function, then you can do musch more things with `AddHostObject`.
