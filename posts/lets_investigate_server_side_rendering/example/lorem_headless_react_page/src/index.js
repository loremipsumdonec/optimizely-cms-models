import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';
import { initStore } from './Components/ApplicationState';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';

let render = ReactDOM.render;

let state = {
}

if(window.__model) {

  state = {
    api : {
      url: '/'
    },
    model: {
      model: window.__model
    }
  }
  
  render = ReactDOM.hydrate;
}

render(
  <Provider store={initStore(state)}>
    <App Router={BrowserRouter} />
  </Provider>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
