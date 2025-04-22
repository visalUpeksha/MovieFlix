import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Container, Typography, Paper } from '@mui/material';
import Navbar from '../components/Navbar';
import axios from 'axios';


interface Movie {
    movieId: number;
    movieName: string;
    rentalCost: number;
    rentalDuration: number;
}

const MovieDetailPage: React.FC = () => {
    const { id } = useParams(); // Get the movieId from the URL parameter
    const [movie, setMovie] = useState<Movie | null>(null);

    // Fetch movie details based on the id
    useEffect(() => {
        const fetchMovieDetail = async () => {
            try {
                const response = await axios.get(`https://localhost:44346/api/movies/${id}`, {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem('token')}`,
                    },
                });
                setMovie(response.data);
            } catch (error) {
                console.error('Error fetching movie details:', error);
            }
        };

        if (id) {
            fetchMovieDetail();
        }
    }, [id]);

    if (!movie) {
        return <Typography variant="h6">Loading movie details...</Typography>;
    }

    return (
        <>
            <Navbar />
            <Container sx={{ mt: 4 }}>
                <Paper sx={{ p: 3 }}>
                    <Typography variant="h4">{movie.movieName}</Typography>
                    <Typography variant="subtitle1">Rental Cost: ${movie.rentalCost}</Typography>
                    <Typography variant="subtitle1">Rental Duration: {movie.rentalDuration} days</Typography>
                </Paper>
            </Container>
        </>
    );
};

export default MovieDetailPage;
