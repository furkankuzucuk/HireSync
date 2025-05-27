// src/App.tsx
import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate, useNavigate, useParams } from 'react-router-dom';

import LoginPage from './pages/LoginPage';
import AdminDashboard from './pages/AdminDashboard';
import JobPostingManagement from './pages/JobPostingManagement';
import OnlineExam from './pages/OnlineExam';
import PerformanceAnalysis from './pages/PerformanceAnalysis';
import LeaveRequests from './pages/LeaveRequests';
import JobListAdmin from './pages/JobListAdmin';
import JobApplications from './pages/JobApplications';
import AdminAnnouncements from './pages/AdminAnnouncements'; // ✅ EKLENDİ

import WorkerDashboard from './pages/WorkerDashboard';
import WorkerAnnouncements from './pages/WorkerAnnouncements';
import WorkerLeaveRequest from './pages/WorkerLeaveRequest';
import WorkerSurveys from './pages/WorkerSurveys';
import MyLeaveRequests from './pages/MyLeaveRequests';

import CandidateDashboard from './pages/CandidateDashboard';
import CandidateRegisterPage from './pages/CandidateRegisterPage';
import ApplicationStatus from './pages/ApplicationStatus';
import JobListings from './pages/JobListings';
import UploadResume from './pages/UploadResume';

import ForgotPassword from './pages/ForgotPassword';
import ResetPassword from './pages/ResetPassword';
import AdminHome from './pages/AdminHome';
import WorkerHome from './pages/WorkerHome';
import CandidateHome from './pages/CandidateHome';
import ProtectedRoute from './pages/ProtectedRoute';
import LandingPage from './pages/LandingPage';
import JobDetails from './pages/JobDetails';

import SurveyList from './pages/SurveyList';
import SurveyDetail from './pages/SurveyDetail';
import SurveyResults from './pages/SurveyResults';
import SurveyAdminList from './pages/SurveyAdminList';
import SurveyCreate from './pages/SurveyCreate';
import SurveyAddQuestion from './pages/SurveyAddQuestion';
import SurveyEdit from './pages/SurveyEdit';
import ExamList from './pages/ExamList';
import SubmitExam from './pages/SubmitExam';
import UserExamResults from './pages/UserExamResult';
import PerformanceReviewPage from './pages/PerformanceReview';

const LoginPageWrapper = () => {
  const navigate = useNavigate();
  const handleLoginSuccess = (role: string) => {
    if (role === "Admin") navigate('/admin-dashboard');
    else if (role === "Worker") navigate('/worker-dashboard');
    else if (role === "Candidate") navigate('/candidate-dashboard');
    else alert("Yetkisiz kullanıcı rolü.");
  };
  return <LoginPage onLoginSuccess={handleLoginSuccess} />;
};

const SubmitExamWrapper = () => {
  const { id } = useParams();
  return <SubmitExam examId={parseInt(id || '0')} />;
};

const App = () => {
  return (
    <Router>
      <Routes>
        {/* 🔓 Public Routes */}
        <Route path="/" element={<LandingPage />} />
        <Route path="/login" element={<LoginPageWrapper />} />
        <Route path="/register" element={<CandidateRegisterPage />} />
        <Route path="/forgot-password" element={<ForgotPassword />} />
        <Route path="/reset-password" element={<ResetPassword />} />
        <Route path="/job-details/:id" element={<JobDetails />} />

        {/* 🔒 Protected Routes */}
        <Route element={<ProtectedRoute />}>
          {/* ✅ Admin Routes */}
          <Route path="/admin-dashboard" element={<AdminDashboard />}>
            <Route index element={<AdminHome />} />
            <Route path="jobs" element={<JobPostingManagement />} />
            <Route path="joblist" element={<JobListAdmin />} />
            <Route path="exams" element={<OnlineExam />} />
            <Route path="performance" element={<PerformanceAnalysis />} />
            <Route path="performance-review" element={<PerformanceReviewPage />} />
            <Route path="survey-results" element={<SurveyAdminList />} />
            <Route path="survey-results/:id" element={<SurveyResults />} />
            <Route path="survey-create" element={<SurveyCreate />} />
            <Route path="survey-add-question/:id" element={<SurveyAddQuestion />} />
            <Route path="survey-edit/:id" element={<SurveyEdit />} />
            <Route path="leaves" element={<LeaveRequests />} />
            <Route path="jobapplications" element={<JobApplications />} />
            <Route path="announcements" element={<AdminAnnouncements />} /> {/* ✅ EKLENDİ */}
          </Route>

          {/* ✅ Worker Routes */}
          <Route path="/worker-dashboard" element={<WorkerDashboard />}>
            <Route index element={<WorkerHome />} />
            <Route path="leave" element={<WorkerLeaveRequest />} />
            <Route path="leave-history" element={<MyLeaveRequests />} />
            <Route path="training" element={<ExamList />} />
            <Route path="exam/:id" element={<SubmitExamWrapper />} />
            <Route path="exam-results" element={<UserExamResults />} />
            <Route path="surveys" element={<SurveyList />} />
            <Route path="surveys/:id" element={<SurveyDetail />} />
            <Route path="surveys/results" element={<SurveyResults />} />
            <Route path="announcements" element={<WorkerAnnouncements />} />
          </Route>

          {/* ✅ Candidate Routes */}
          <Route path="/candidate-dashboard" element={<CandidateDashboard />}>
            <Route index element={<JobListings />} />
            <Route path="jobs" element={<JobListings />} />
            <Route path="status" element={<ApplicationStatus />} />
            <Route path="upload-resume" element={<UploadResume />} />
          </Route>
        </Route>

        {/* ❌ Fallback */}
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </Router>
  );
};

export default App;
