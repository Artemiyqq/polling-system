import { createSlice } from '@reduxjs/toolkit';
import { registerUser, loginUser, checkToken, logoutUser } from '../actions/authActions';

const authSlice = createSlice({
    name: 'auth',
    initialState: {
        jwt: null as string | null,
        isAuthenticated: false,
        loading: false,
        error: null as string | null,
    },
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(registerUser.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(registerUser.rejected, (state, action) => {
                state.loading = false;
                if (typeof action.payload === 'object' && action.payload !== null && 'message' in action.payload) {
                    state.error = (action.payload as { message: string }).message;
                } else {
                    state.error = 'An unknown error occurred';
                }
            })
            .addCase(registerUser.fulfilled, (state) => {
                state.loading = false;
                state.error = null;
            })

            .addCase(loginUser.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(loginUser.rejected, (state, action) => {
                state.loading = false;
                if (typeof action.payload === 'object' && action.payload !== null && 'message' in action.payload) {
                    state.error = (action.payload as { message: string }).message;
                } else {
                    state.error = 'An unknown error occurred';
                }
            })
            .addCase(loginUser.fulfilled, (state, action) => {
                state.jwt = action.payload;
                state.isAuthenticated = true;
                state.loading = false;
                state.error = null;
            })

            .addCase(checkToken.fulfilled, (state, action) => {
                state.jwt = action.payload;
                state.isAuthenticated = true;
            })
            .addCase(checkToken.rejected, (state) => {
                state.jwt = null;
                state.isAuthenticated = false;
            })
            .addCase(logoutUser.fulfilled, (state) => {
                state.jwt = null;
                state.isAuthenticated = false;
            });
    },
});

export default authSlice.reducer;