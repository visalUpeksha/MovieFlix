import React, { useEffect, useState } from 'react';
import { Grid, Typography, Box } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import Navbar from '../components/Navbar';  


interface Movie {
    movieId: number;
    movieName: string;
    rentalCost: number;
    rentalDuration: number;
}

const MainPage: React.FC = () => {
    const [movies, setMovies] = useState<Movie[]>([]);
    const navigate = useNavigate();

    // Fetch movies from the API
    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const response = await axios.get('https://localhost:44346/api/movies', {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem('token')}`,
                    },
                });
                setMovies(response.data);
            } catch (error) {
                console.error('Error fetching movies:', error);
            }
        };

        fetchMovies();
    }, []);

    // Navigate to movie detail page
    const handleMovieClick = (movieId: number) => {
        navigate(`/movie/${movieId}`);
    };

    return (
        <Box sx={{ padding: '16px' }}>
            <Navbar /> 

            <Typography variant="h4" gutterBottom>
                Movie List
            </Typography>

            <Grid container spacing={2}>
                {movies.map((movie) => (
                    <Grid key={movie.movieId}>
                        <Box
                            sx={{
                                border: '1px solid #ddd',
                                borderRadius: '8px',
                                padding: '16px',
                                textAlign: 'center',
                                cursor: 'pointer',
                                '&:hover': {
                                    backgroundColor: '#f0f0f0',
                                },
                            }}
                            onClick={() => handleMovieClick(movie.movieId)}
                        >
                            <Typography variant="h6">{movie.movieName}</Typography>
                            <Typography variant="body1">Rental Cost: ${movie.rentalCost}</Typography>
                            <Typography variant="body2">Duration: {movie.rentalDuration} days</Typography>
                        </Box>
                    </Grid>
                ))}
            </Grid>
        </Box>
    );
};

export default MainPage;
