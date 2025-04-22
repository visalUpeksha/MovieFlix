import React, { createContext, useContext, useState } from 'react';

export interface Movie {
    movieId: number;
    movieName: string;
    rentalCost: number;
    rentalDuration: number;
}

interface MovieContextType {
    selectedMovie: Movie | null;
    setSelectedMovie: (movie: Movie) => void;
}

const MovieContext = createContext<MovieContextType | undefined>(undefined);

export const MovieProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [selectedMovie, setSelectedMovie] = useState<Movie | null>(null);

    return (
        <MovieContext.Provider value={{ selectedMovie, setSelectedMovie }}>
            {children}
        </MovieContext.Provider>
    );
};

export const useMovieContext = (): MovieContextType => {
    const context = useContext(MovieContext);
    if (!context) {
        throw new Error('useMovieContext must be used within a MovieProvider');
    }
    return context;
};
