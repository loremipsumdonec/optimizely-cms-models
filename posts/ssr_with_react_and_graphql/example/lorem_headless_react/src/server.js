import React from 'react';
import {
	ApolloClient,
	InMemoryCache,
	ApolloProvider,
} from "@apollo/client";
import { Observable } from '@apollo/client/utilities';
import { print } from 'graphql';
import { renderToStringWithData  } from "@apollo/client/react/ssr";
import { ApolloLink } from '@apollo/client';
import Application from './App';

const directCommunicationWithBackend = new ApolloLink((operation, _) => {

	const { operationName, variables, query } = operation;
  
	let body = {
	  operationName,
	  variables,
	  query: print(query)
	}
  
	return new Observable(observer => {

		// eslint-disable-next-line no-undef
		Connector.Execute('https://api.spacex.land/graphql', JSON.stringify(body))
			.then(response => { 
				operation.setContext({response});	

				var result = JSON.parse(response);
				
				observer.next(result);
				observer.complete();
				
				return result;
			});
		});
});

const client = new ApolloClient({
	ssrMode: true,
	link: directCommunicationWithBackend,
	cache: new InMemoryCache(),
});
  
function App() {
	return(
		<React.StrictMode>
			<ApolloProvider client={client}>
				<Application />
			</ApolloProvider>
		</React.StrictMode>
	);
}

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