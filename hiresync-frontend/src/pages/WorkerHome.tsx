import React from 'react';
import useLogout from "./useLogout";
import WorkerDashboard from './WorkerDashboard';



const WorkerHome = () => {
  const logout = useLogout();
  const username = localStorage.getItem('username');
  return (
    <div className="tab-content">
      <h2>🏠 Hoş Geldiniz, {username}</h2>
      <p>Bugün: {new Date().toLocaleDateString("tr-TR")}</p>
      <p>Buradan izin talebi, eğitimler ve duyurulara erişebilirsiniz.</p>
    </div>
  );
};

export default WorkerHome;
