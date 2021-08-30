import React from 'react';
import ReactDOMServer from 'react-dom/server';
import Application from './App';

export const shout = () => {
	return "hello world";
}

export const render = () => {
	var element = React.createElement(Application);
	return ReactDOMServer.renderToStaticMarkup(element);
}