// actions/authActions.ts
import { createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';
import Cookies from 'js-cookie';
import { RegisterProps } from '../types/RegisterProps';
import { LogInProps } from '../types/LogInProps';

const API_URL = import.meta.env.VITE_REACT_APP_API_URL;
const JWT_EXPIRES_AFTER = 14;

export const registerUser = createAsyncThunk('auth/register', async (registerProps: RegisterProps, thunkAPI) => {
    try {
        const response = await axios.post(`${API_URL}/auth/register`, registerProps);
        return response.data;
    } catch (error: any) {
        return thunkAPI.rejectWithValue(error.response?.data || 'Something went wrong');
    }
});

export const loginUser = createAsyncThunk('auth/login', async (loginData: LogInProps, thunkAPI) => {
    try {
        const response = await axios.post<{ jwt: string, message: string }>(`${API_URL}/auth/log-in`, loginData);
        Cookies.set('jwt', response.data.jwt, { expires: loginData.rememberMe ? JWT_EXPIRES_AFTER : undefined })
        return response.data.message!;
    } catch (error: any) {
        return thunkAPI.rejectWithValue(error.response?.data || 'Something went wrong');
    }
});

export const checkToken = createAsyncThunk('auth/checkToken', async (_, thunkAPI) => {
    const token = Cookies.get('jwt');
    if (token) return token;
    return thunkAPI.rejectWithValue('No token found');
});

export const logoutUser = createAsyncThunk('auth/logout', async () => {
    Cookies.remove('jwt');
});
