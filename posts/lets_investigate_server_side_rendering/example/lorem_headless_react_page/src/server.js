import React from 'react';
import { StaticRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { initStore } from './Components/ApplicationState';
import ReactDOMServer from 'react-dom/server';
import Application from './App';
import './index.css';

export const render = (model, entrypoints) => {
	
	var scripts = [];
	var styles = []

	for (const entrypoint of entrypoints) {
		if(entrypoint.endsWith('.css')) {
			styles.push(`<link rel="stylesheet" href="${entrypoint}">`)
			continue;
		}

		scripts.push(`<script src="${entrypoint}"></script>`)
	}

	return ` 
	<html>
		<head>
			${styles.join('\n')}
		</head>
		<body>
			<script>
				window.__model = ${JSON.stringify(model)}
			</script>
			<div id="root">
				${ReactDOMServer.renderToString(React.createElement(App, model))}
			</div>
			${scripts.join('\n')}
		</body>
	</html>`
}

const App = (model) => {
	
	const state = {
		api: {
			url: '/'
		},
		model: { 
			model 
		}
	}

	return (
		<Provider store={initStore(state)}>
			<Application Router={StaticRouter} />
		</Provider>
	);
}