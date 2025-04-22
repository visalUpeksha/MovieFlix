import React from 'react';
import { Navigate } from 'react-router-dom';

interface Props {
    children: React.ReactNode;
}

const ProtectedRoute: React.FC<Props> = ({ children }) => {
    // Check if the JWT token is available in localStorage
    const token = localStorage.getItem('token');

    // If there is no token, redirect to the login page
    if (!token) {
        return <Navigate to="/login" />;
    }

    // If there's a token, render the protected content
    return <>{children}</>;
};

export default ProtectedRoute;
