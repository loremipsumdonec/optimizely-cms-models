import React from 'react';
import ReactDOMServer from 'react-dom/server';
import Application from './App';

export const render = (entrypoints) => {
	
	var files = [];

	for (const entrypoint of entrypoints) {
		if(entrypoint.endsWith('.css')) {
			files.push(`<link rel="stylesheet" href="${entrypoint}">`)
			continue;
		}

		files.push(`<script src="${entrypoint}"></script>`)
	}

	return ` 
	<html>
		<head>
			${files.join('\n')}
		</head>
		<body>
			${ReactDOMServer.renderToStaticMarkup(React.createElement(Application))}
		</body>
	</html>`
}