import React from 'react';
import { Card, CardContent, Typography, CardActionArea } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { Movie } from '../context/MovieContext';

const MovieCard: React.FC<{ movie: Movie; onSelect: () => void }> = ({ movie, onSelect }) => {
    const navigate = useNavigate();

    const handleClick = () => {
        onSelect();
        navigate(`/movie/${movie.movieId}`);
    };

    return (
        <Card>
            <CardActionArea onClick={handleClick}>
                <CardContent>
                    <Typography variant="h6">{movie.movieName}</Typography>
                    <Typography variant="body2">Cost: ${movie.rentalCost}</Typography>
                    <Typography variant="body2">Duration: {movie.rentalDuration} days</Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    );
};

export default MovieCard;
