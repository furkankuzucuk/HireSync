import React, { useState } from 'react';
import LoginPage from './pages/LoginPage';
import AdminDashboard from './pages/AdminDashboard';
import WorkerDashboard from './pages/WorkerDashboard';
import CandidateDashboard from './pages/CandidateDashboard';

const App = () => {
  const [role, setRole] = useState<string | null>(null); // Kullanıcı rolünü sakla

  const handleLoginSuccess = (role: string) => {
    setRole(role); // Giriş başarılıysa rolü kaydet
  };

  return (
    <div className="App">
      {!role ? (
        <LoginPage onLoginSuccess={handleLoginSuccess} />
      ) : role === "Admin" ? (
        <AdminDashboard />
      ) : role === "Worker" ? (
        <WorkerDashboard />
      ) 
      : role == "Candidate" ? (

        <CandidateDashboard/>
      )
      
      : (
        <div>Yetkisiz erişim</div>
      )}
    </div>
  );
};

export default App;
