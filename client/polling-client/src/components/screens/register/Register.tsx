import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import * as Yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { TextField, Button, Container, Typography, Alert } from '@mui/material';
import { useAppDispatch, useAppSelector } from '../../../main';
import { registerUser } from '../../../actions/authActions';
import { RootState } from '../../../store';

const Register: React.FC = () => {
    const [success, setSuccess] = useState<string | null>(null);
    const dispatch = useAppDispatch();
    const { loading, error } = useAppSelector((state: RootState) => state.auth);

    const validationSchema = Yup.object().shape({
        username: Yup.string()
            .min(3, 'Must be at least 3 characters')
            .required('Required'),
        email: Yup.string()
            .email('Invalid email address')
            .required('Required'),
        password: Yup.string()
            .min(8, 'Must be at least 8 characters')
            .required('Required'),
    });

    const { register, handleSubmit, formState: { errors } } = useForm({
        resolver: yupResolver(validationSchema)
    });

    const onSubmit = async (data: any) => {
        const resultAction = await dispatch(registerUser(data));

        if (registerUser.fulfilled.match(resultAction)) {
            const response = resultAction.payload;
            setSuccess(response);
        }
    };

    return (
        <Container sx={{ background: "white", boxShadow: 2, borderRadius: 2, padding: 3, width: '500px' }}>
            <Typography variant="h4" component="h1" align="center">
                Register Form
            </Typography>
            {error && <Alert severity="error" sx={{ fontSize: 16 }}>{error}</Alert>}
            {loading && <Typography sx={{ textAlign: 'center' }}>Loading...</Typography>}
            <form onSubmit={handleSubmit(onSubmit)}>
                <TextField
                    id="username"
                    label="Username"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register('username')}
                    error={!!errors.username}
                    helperText={errors.username ? errors.username.message : ''}
                />
                <TextField
                    id="email"
                    label="Email"
                    type="email"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register('email')}
                    error={!!errors.email}
                    helperText={errors.email ? errors.email.message : ''}
                />
                <TextField
                    id="password"
                    label="Password"
                    type="password"
                    variant="outlined"
                    fullWidth
                    margin="normal"
                    {...register('password')}
                    error={!!errors.password}
                    helperText={errors.password ? errors.password.message : ''}
                />
                <Button type="submit" variant="contained" color="info" fullWidth sx={{ marginTop: 1, fontSize: 18 }}>
                    Register
                </Button>
            </form>
            {success && <Alert severity="success" sx={{ mt: 2, fontSize: 16 }}>{success}</Alert>}
            <Typography mt={2} variant="h6" align="center">Already have an account? <a href="/log-in">Log In</a></Typography>
        </Container>
    );
};

export default Register;
