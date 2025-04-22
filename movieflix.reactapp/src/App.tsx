import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import MainPage from './pages/MainPage'; 
import MovieDetailPage from './pages/MovieDetailPage';
import ProtectedRoute from './routes/ProtectedRoute'; 

const App: React.FC = () => {
    const token = localStorage.getItem('token');  // Check if there's a token

    return (
        <Router>
            <Routes>
                {/* Login Page - Redirect to MainPage if the user is already logged in */}
                <Route path="/login" element={token ? <Navigate to="/main" /> : <LoginPage />} />
                
                <Route path="/main" element={<ProtectedRoute><MainPage /></ProtectedRoute>} />
                <Route path="/movie/:id" element={<ProtectedRoute><MovieDetailPage /></ProtectedRoute>} />
                {/* Redirect the root to login if no token */}
                <Route path="/" element={token ? <Navigate to="/main" /> : <Navigate to="/login" />} />
            </Routes>
        </Router>
    );
};

export default App;
