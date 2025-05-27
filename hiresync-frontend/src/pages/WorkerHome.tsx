import React from 'react';
import useLogout from "./useLogout";
import "../css/WorkerHome.css";

const WorkerHome: React.FC = () => {
  const logout = useLogout();
  const username = localStorage.getItem('username') || "Kullanıcı";

  return (
    <div className="tab-content p-4">
      <h2>🏠 Hoş Geldiniz, {username}</h2>
      <p>Bugün: {new Date().toLocaleDateString("tr-TR")}</p>
      <p>Buradan izin talebi, eğitimler ve duyurulara erişebilirsiniz.</p>
    </div>
  );
};

export default WorkerHome;
