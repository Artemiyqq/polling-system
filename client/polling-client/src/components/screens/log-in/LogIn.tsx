import React from 'react';
import { useForm, Controller } from 'react-hook-form';
import { TextField, Button, Container, Typography, Box, Checkbox, FormControlLabel, FormHelperText, Alert } from '@mui/material';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { LogInProps } from '../../../types/LogInProps';
import { useAppDispatch, useAppSelector } from '../../../main';
import { RootState } from '../../../store';
import { loginUser } from '../../../actions/authActions';



const LogIn: React.FC = () => {
    const { loading, error } = useAppSelector((state: RootState) => state.auth) as {
        loading: boolean, error: string | { message: string } | null 
    };
    const dispatch = useAppDispatch();


    const schema = yup.object().shape({
        email: yup.string().email('Invalid email').required('Email is required'),
        password: yup.string().min(8, 'Password must be at least 8 characters').required('Password is required'),
        rememberMe: yup.boolean().required('Remember me is required'),
    });

    const { control, handleSubmit, formState: { errors } } = useForm<LogInProps>({
        resolver: yupResolver(schema),
    });

    const onSubmit = async (data: LogInProps) => {
        await dispatch(loginUser(data));
    };

    return (
        <Container sx={{ background: "white", boxShadow: 2, borderRadius: 2, padding: 3, width: '500px' }}>
            <Typography variant="h4" component="h1" align="center" >
                Log In Form
            </Typography>
            {error && (
                <Alert severity="error" sx={{ mt: 2, fontSize: 16 }}>
                    {typeof error === 'string' ? error : error.message || 'An error occurred'}
                </Alert>
            )}
            {loading && <Typography sx={{ textAlign: 'center' }}>Loading...</Typography>}
            <form onSubmit={handleSubmit(onSubmit)}>
                <Controller
                    name="email"
                    control={control}
                    defaultValue=""
                    render={({ field }) => (
                        <TextField
                            {...field}
                            label="Email"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            error={!!errors.email}
                            helperText={errors.email ? errors.email.message : ''}
                        />
                    )}
                />
                <Controller
                    name="password"
                    control={control}
                    defaultValue=""
                    render={({ field }) => (
                        <TextField
                            {...field}
                            label="Password"
                            type="password"
                            variant="outlined"
                            margin="normal"
                            fullWidth
                            error={!!errors.password}
                            helperText={errors.password ? errors.password.message : ''}
                        />
                    )}
                />
                <Controller
                    name="rememberMe"
                    control={control}
                    defaultValue={false}
                    render={({ field }) => (
                        <Box>
                            <FormControlLabel
                                control={<Checkbox {...field} checked={field.value} />}
                                label="Remember Me"
                                labelPlacement="end"
                                sx={{ padding: 0 }}
                            />
                            {errors.rememberMe && (
                                <FormHelperText error>{errors.rememberMe.message}</FormHelperText>
                            )}
                        </Box>
                    )}
                />
                <Button type="submit" variant="contained" fullWidth color='info' sx={{ marginTop: 1, fontSize: 18 }}>
                    Log In
                </Button>
            </form>
            <Typography mt={2} variant='h6' align='center'>Don't have an account? <a href="/register">Register</a></Typography>
        </Container>
    );
};

export default LogIn;