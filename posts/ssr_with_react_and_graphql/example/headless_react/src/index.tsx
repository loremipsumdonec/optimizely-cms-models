import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter } from 'react-router-dom';
import { ApolloClient, InMemoryCache } from '@apollo/client';

declare global {
  interface Window {
    __model : any,
    __APOLLO_STATE: any; 
  }
}

let render = ReactDOM.render;
let model = {
  content: {
  }
}

if (window.__model) {
	render = ReactDOM.hydrate;
	model = window.__model;
}

const client = new ApolloClient({
  uri: 'http://localhost:59590/api/headless/graphql',
  cache:window.__APOLLO_STATE ? new InMemoryCache().restore(window.__APOLLO_STATE) : new InMemoryCache(),
});

render(
  <React.StrictMode>
    <App client={client} model={model} Router={BrowserRouter}  />
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
