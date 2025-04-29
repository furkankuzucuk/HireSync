import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate, useNavigate } from 'react-router-dom';

import LoginPage from './pages/LoginPage';
import AdminDashboard from './pages/AdminDashboard';
import JobPostingManagement from './pages/JobPostingManagement';
import OnlineExam from './pages/OnlineExam';
import PerformanceAnalysis from './pages/PerformanceAnalysis';
import LeaveRequests from './pages/LeaveRequests';

import WorkerDashboard from './pages/WorkerDashboard';
import WorkerAnnouncements from './pages/WorkerAnnouncements';
import WorkerLeaveRequest from './pages/WorkerLeaveRequest';
import WorkerSurveys from './pages/WorkerSurveys';
import WorkerTraining from './pages/WorkerTraining';

import CandidateDashboard from './pages/CandidateDashboard';
import ApplicationStatus from './pages/ApplicationStatus';
import JobListings from './pages/JobListings';

import ForgotPassword from './pages/ForgotPassword';
import ResetPassword from './pages/ResetPassword';
import AdminHome from './pages/AdminHome';
import WorkerHome from './pages/WorkerHome';
import CandidateHome from './pages/CandidateHome';
import ProtectedRoute from './pages/ProtectedRoute';
import LandingPage from './pages/LandingPage';




// Login sonrası yönlendirme component'ı
const LoginPageWrapper = () => {
  const navigate = useNavigate();
  const [loginState, setLoginState] = useState<'idle' | 'success'>('idle');

  const handleLoginSuccess = (role: string) => {
    if (role === "Admin") {
      navigate('/admin-dashboard');
    } else if (role === "Worker") {
      navigate('/worker-dashboard');
    } else if (role === "Candidate") {
      navigate('/candidate-dashboard');
    } else {
      alert("Yetkisiz kullanıcı rolü.");
    }
    setLoginState('success');
  };

  return <LoginPage onLoginSuccess={handleLoginSuccess} />;
};

const App = () => {
  return (
    <Router>
      <Routes>
      <Route path="/" element={<LandingPage />} />
        {/* Landing Page */}
        <Route path="/" element={<LandingPage />} />
        
        {/* Login */}
        <Route path="/login" element={<LoginPageWrapper />} />
        
        {/* Protected Routes */}
        <Route element={<ProtectedRoute />}>
          
          {/* Admin Nested Routes */}
          <Route path="/admin-dashboard" element={<AdminDashboard />}>
            <Route index element={<AdminHome />} />
            <Route path="jobs" element={<JobPostingManagement />} />
            <Route path="exams" element={<OnlineExam />} />
            <Route path="performance" element={<PerformanceAnalysis />} />
            <Route path="leaves" element={<LeaveRequests />} />
          </Route>

          {/* Worker Nested Routes */}
          <Route path="/worker-dashboard" element={<WorkerDashboard />}>
            <Route index element={<WorkerHome />} />
            <Route path="leave" element={<WorkerLeaveRequest />} />
            <Route path="training" element={<WorkerTraining />} />
            <Route path="surveys" element={<WorkerSurveys />} />
            <Route path="announcements" element={<WorkerAnnouncements />} />
          </Route>

          {/* Candidate Nested Routes */}
          <Route path="/candidate-dashboard" element={<CandidateDashboard />}>
            <Route index element={<CandidateHome />} />
            <Route path="jobs" element={<JobListings />} />
            <Route path="status" element={<ApplicationStatus />} />
          </Route>

        </Route>

        {/* Forgot Password + Reset */}
        <Route path="/forgot-password" element={<ForgotPassword />} />
        <Route path="/reset-password" element={<ResetPassword />} />

        {/* Yakalanmayan URL'ler */}
        <Route path="*" element={<Navigate to="/" replace />} />

      </Routes>
    </Router>
  );
};

export default App;
