import { configureStore, combineReducers } from '@reduxjs/toolkit';
import api from './api';
import model from './model';
import { getModelStart, getModelSuccess, getModelFailed } from './model';

export const selectApiUrl = (state) => state.api.url;
export const selectModel = (state) => state.model;
export const selectContent = (state) => state.model.model?.content;

export {
	getModelStart,
	getModelSuccess,
	getModelFailed
} 

const rootReducer = combineReducers({
    api,
    model
});

export const initStore = (preloadedState) => {
    return configureStore({
        reducer: rootReducer,
        preloadedState
    });
}