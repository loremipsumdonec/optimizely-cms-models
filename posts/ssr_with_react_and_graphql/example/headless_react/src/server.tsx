import React from 'react';
import { StaticRouter } from 'react-router-dom';
import {
	ApolloClient,
	InMemoryCache
} from "@apollo/client";
import { Observable } from '@apollo/client/utilities';
import { print } from 'graphql';
import { renderToStringWithData  } from "@apollo/client/react/ssr";
import { ApolloLink } from '@apollo/client';
import App from './App';

const createDirectCommunicationWithBackend = (connector:any) => {
    return new ApolloLink((operation, _) => {

        const { operationName, variables, query } = operation;
      
        let body = {
          operationName,
          variables,
          query: print(query)
        }
      
        return new Observable(observer => {
    
            connector.Execute('', JSON.stringify(body))
                .then((response:any) => { 
                    operation.setContext({response});	
    
                    var result = JSON.parse(response);
                    
                    observer.next(result);
                    observer.complete();
                    
                    return result;
                });
            });
    });
}


export const render = async (model:any, entrypoints:any, connector:any) => {

    const client = new ApolloClient({
        ssrMode: true,
        link: createDirectCommunicationWithBackend(connector),
        cache: new InMemoryCache(),
    });

    const Application = () =>
        <React.StrictMode>
            <App client={client} model={model} Router={StaticRouter}  />
        </React.StrictMode>


	const applicationContent = await renderToStringWithData(React.createElement(Application));
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
			<head></head>
			<body>
                <script>
                    window.__model = ${JSON.stringify(model)}
                    window.__APOLLO_STATE=${JSON.stringify(initialApolloClientState).replace(/</g, '\\u003c')}
                </script>
                <div id="root">
                    ${applicationContent}
                </div>
                ${files.join('\n')}
			</body>
		</html>`

	connector.Send(html);
}