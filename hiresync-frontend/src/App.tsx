import React, { useEffect, useState } from 'react';
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
import JobDetails from './pages/JobDetails';
import CandidateRegisterPage from './pages/CandidateRegisterPage';

import SurveyForm from './pages/SurveyForm';
import SurveyResult from './pages/SurveyResult';
import { getSurveyQuestions } from './services/SurveyService';

// ✅ Login sonrası yönlendirme component'ı
const LoginPageWrapper = () => {
  const navigate = useNavigate();

  const handleLoginSuccess = (role: string) => {
    if (role === "Admin") {
      navigate('/admin-dashboard');
    } else if (role === "Worker") {
      navigate('/worker-dashboard');
    } else if (role === "Candidate") {
      navigate('/candidate-dashboard');
    }
    else {
      alert("Yetkisiz kullanıcı rolü.");
    }
  };

  return <LoginPage onLoginSuccess={handleLoginSuccess} />;
};

const App = () => {
  const [surveyId, setSurveyId] = useState(1); // Example survey ID
  const [questions, setQuestions] = useState([]);

  useEffect(() => {
    const fetchQuestions = async () => {
      try {
        const response = await getSurveyQuestions(surveyId); // Fetch survey questions
        setQuestions(response);
      } catch (error) {
        alert('Error fetching survey questions.');
      }
    };
    fetchQuestions();
  }, [surveyId]);

  return (
    <Router>
      <Routes>

        {/* 🔓 General public pages */}
        <Route path="/" element={<LandingPage />} />
        <Route path="/login" element={<LoginPageWrapper />} />
        <Route path="/job-details/:id" element={<JobDetails />} />
        <Route path="/register" element={<CandidateRegisterPage />} /> {/* ✅ Candidate Register page */}
        <Route path="/forgot-password" element={<ForgotPassword />} />
        <Route path="/reset-password" element={<ResetPassword />} />

        {/* 🔒 Protected routes */}
        <Route element={<ProtectedRoute />}>
          {/* Admin Routes */}
          <Route path="/admin-dashboard" element={<AdminDashboard />}>
            <Route index element={<AdminHome />} />
            <Route path="jobs" element={<JobPostingManagement />} />
            <Route path="exams" element={<OnlineExam />} />
            <Route path="performance" element={<PerformanceAnalysis />} />
            <Route path="leaves" element={<LeaveRequests />} />
          </Route>

          {/* Worker Routes */}
          <Route path="/worker-dashboard" element={<WorkerDashboard />}>
            <Route index element={<WorkerHome />} />
            <Route path="leave" element={<WorkerLeaveRequest />} />
            <Route path="training" element={<WorkerTraining />} />
            <Route path="surveys" element={<WorkerSurveys />} />
            <Route path="announcements" element={<WorkerAnnouncements />} />
          </Route>

          {/* Candidate Routes */}
          <Route path="/candidate-dashboard" element={<CandidateDashboard />}>
            <Route index element={<CandidateHome />} />
            <Route path="jobs" element={<JobListings />} />
            <Route path="status" element={<ApplicationStatus />} />
          </Route>

          {/* Survey Routes */}
          <Route path="/survey/:surveyId" element={<SurveyForm surveyId={surveyId} questions={questions} />} />
          <Route path="/survey-result/:surveyId" element={<SurveyResult surveyId={surveyId} />} />
        </Route>

        {/* ❌ Invalid route handling */}
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </Router>
  );
};

export default App;
