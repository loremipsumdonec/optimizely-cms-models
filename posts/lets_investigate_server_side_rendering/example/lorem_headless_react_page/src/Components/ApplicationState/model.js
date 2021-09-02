import { createSlice } from '@reduxjs/toolkit';

const slice = createSlice({
    name: 'model',
    initialState: {
        error: null,
        loading: false,
        model: {}
    },
    reducers: {
        getModelStart: (state) => {
            state.loading = true
        },
        getModelSuccess: (state, action) => {
            state.model = action.payload;
            state.loading = false
        },
        getModelFailed: (state, action) => {
			state.error = action.payload;
			state.loading = false;
		},
    }
})

export const {
	getModelStart,
	getModelSuccess,
	getModelFailed
} = slice.actions;

export default slice.reducer;