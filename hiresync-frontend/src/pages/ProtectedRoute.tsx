// src/pages/ProtectedRoute.tsx
import React from 'react';
import { Navigate, Outlet, useLocation } from 'react-router-dom';

const ProtectedRoute = () => {
  const token = localStorage.getItem('token');
  const role = localStorage.getItem('role');
  const expiration = localStorage.getItem('tokenExpiration');
  const location = useLocation();

  // Token süresi kontrolü
  if (expiration && Date.now() > Number(expiration)) {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('username');
    localStorage.removeItem('tokenExpiration');
    return <Navigate to="/" replace />;
  }

  if (!token) {
    return <Navigate to="/" replace />;
  }

  // Role bazlı erişim kontrolü
  if (location.pathname.startsWith('/admin-dashboard') && role !== 'Admin') {
    return <Navigate to="/" replace />;
  }

  if (location.pathname.startsWith('/worker-dashboard') && role !== 'Worker') {
    return <Navigate to="/" replace />;
  }

  if (location.pathname.startsWith('/candidate-dashboard') && role !== 'Candidate') {
    return <Navigate to="/" replace />;
  }

  return <Outlet />;
};

export default ProtectedRoute;
