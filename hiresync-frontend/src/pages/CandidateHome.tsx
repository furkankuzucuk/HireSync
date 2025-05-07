import React from 'react';
import useLogout from "./useLogout";
import CandidateDashboard from './CandidateDashboard';

const CandidateHome = () => {
  const logout = useLogout();
  const username = localStorage.getItem('username');
  return (
    <div className="tab-content">
      <h2>📌 Hoş Geldiniz, {username}</h2>
      <p>Bugün: {new Date().toLocaleDateString("tr-TR")}</p>
      <p>Buradan açık pozisyonlara göz atabilir veya başvurularınızı takip edebilirsiniz.</p>
    </div>
  );
};

export default CandidateHome;
