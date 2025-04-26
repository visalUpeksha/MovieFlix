import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Visibility, VisibilityOff } from '@mui/icons-material';
import { Container, TextField, Button, Paper, Typography, Box, IconButton, InputAdornment } from '@mui/material';
import { motion } from 'framer-motion'; 

const LoginPage: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState<string>('');
    const navigate = useNavigate();
    const [showPassword, setShowPassword] = useState(false);
    const [attemptsLeft, setAttemptsLeft] = useState(5);
    const [lockoutTime, setLockoutTime] = useState<Date | null>(null);
    const [remainingTime, setRemainingTime] = useState<string>('');

    useEffect(() => {
        if (!lockoutTime) {
            setRemainingTime('');
            return;
        }

        const interval = setInterval(() => {
            const now = new Date();
            const diff = lockoutTime.getTime() - now.getTime();

            if (diff <= 0) {
                clearInterval(interval);
                setLockoutTime(null);
                setAttemptsLeft(5);
                setRemainingTime('');
                setError('You can try logging in again.');
            } else {
                const minutes = Math.floor(diff / 1000 / 60);
                const seconds = Math.floor((diff / 1000) % 60);
                setRemainingTime(`${minutes}:${seconds.toString().padStart(2, '0')}`);
                setError(`Account locked. Try again in ${minutes}:${seconds.toString().padStart(2, '0')}`);
            }
        }, 1000);

        return () => clearInterval(interval);
    }, [lockoutTime]);


    const handleTogglePasswordVisibility = () => {
        setShowPassword((prev) => !prev);
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (lockoutTime && lockoutTime > new Date()) {
            setError(`Account is locked until ${lockoutTime.toLocaleTimeString()}`);
            return;
        }

        try {
            const response = await axios.post('https://localhost:44346/api/Login', { email, password });
            const { token } = response.data;

            localStorage.setItem('token', token);
            setError('');
            setAttemptsLeft(5);
            setLockoutTime(null);

            navigate('/main');
        } catch (err) {
            const newAttempts = attemptsLeft - 1;
            setAttemptsLeft(newAttempts);

            if (newAttempts <= 0) {
                const lockUntil = new Date();
                lockUntil.setMinutes(lockUntil.getMinutes() + 15);
                setLockoutTime(lockUntil);
                setError(`"Account locked. Try again in ${remainingTime}.`);
            } else {
                setError(`Invalid credentials. ${newAttempts} attempts remaining.`);
            }
        }
    };

    return (
        <Container component="main" maxWidth="xs" sx={{ height: '100vh', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
            <motion.div initial={{ opacity: 0 }} animate={{ opacity: 1 }} transition={{ duration: 0.5 }}>
                <Paper elevation={6} sx={{ padding: 4, borderRadius: 3 }}>
                    <Typography variant="h5" component="h1" align="center" gutterBottom>
                        Welcome Back
                    </Typography>
                    <Box component="form" onSubmit={handleSubmit} noValidate>
                        <TextField
                            margin="normal"
                            label="Email"
                            type="email"
                            fullWidth
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                        />
                        <TextField
                            margin="normal"
                            label="Password"
                            type={showPassword ? "text" : "password"}
                            fullWidth
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                            InputProps={{
                                endAdornment: (
                                    <InputAdornment position="end">
                                        <IconButton
                                            onClick={handleTogglePasswordVisibility}
                                            edge="end"
                                        >
                                            {showPassword ? <VisibilityOff /> : <Visibility />}
                                        </IconButton>
                                    </InputAdornment>
                                ),
                            }}
                        />
                        {error && (
                            <Typography color="error" sx={{ mt: 1, mb: 2 }}>
                                {error}
                            </Typography>
                        )}
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 2, mb: 2, py: 1.5, fontWeight: 'bold' }}
                        >
                            Login
                        </Button>
                    </Box>
                </Paper>
            </motion.div>
        </Container>
    );
};

export default LoginPage;
