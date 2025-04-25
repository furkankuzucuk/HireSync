import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import LoginPage from './pages/LoginPage';
import AdminDashboard from './pages/AdminDashboard';
import WorkerDashboard from './pages/WorkerDashboard';
import CandidateDashboard from './pages/CandidateDashboard';
import ForgotPassword from './pages/ForgotPassword';
import ResetPassword from './pages/ResetPassword';

const App = () => {
  const [role, setRole] = useState<string | null>(null);

  const handleLoginSuccess = (role: string) => {
    setRole(role);
  };

  return (
    <Router>
      <Routes>
        <Route
          path="/"
          element={
            !role ? (
              <LoginPage onLoginSuccess={handleLoginSuccess} />
            ) : role === "Admin" ? (
              <AdminDashboard />
            ) : role === "Worker" ? (
              <WorkerDashboard />
            ) : role === "Candidate" ? (
              <CandidateDashboard />
            ) : (
              <div>Yetkisiz Erişim</div>
            )
          }
        />
        <Route path="/forgot-password" element={<ForgotPassword />} />
        <Route path="/reset-password" element={<ResetPassword />} />
      </Routes>
    </Router>
  );
};

export default App;