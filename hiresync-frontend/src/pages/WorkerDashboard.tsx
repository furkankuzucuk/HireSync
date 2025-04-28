// src/pages/WorkerDashboard.tsx
import React from "react";
import { Outlet, Link } from "react-router-dom";
import useLogout from "./useLogout";
import "../css/WorkerDashboard.css";

const WorkerDashboard = () => {
  const logout = useLogout();
  const username = localStorage.getItem('username');

  return (
    <div className="worker-dashboard">
      <aside className="sidebar">
        <h2>Çalışan Paneli</h2>
        <p style={{ color: "white", padding: "10px" }}>Hoş Geldin, {username} 👋</p>
        <ul>
          <li><Link to="/worker-dashboard">🏠 Ana Sayfa</Link></li>
          <li><Link to="/worker-dashboard/leave">📅 İzin Talebi</Link></li>
          <li><Link to="/worker-dashboard/training">📚 Eğitimler ve Sınavlar</Link></li>
          <li><Link to="/worker-dashboard/surveys">📝 Memnuniyet Anketleri</Link></li>
          <li><Link to="/worker-dashboard/announcements">📢 Duyurular</Link></li>
          <li><button onClick={logout} className="logout-button">🚪 Çıkış Yap</button></li>
        </ul>
      </aside>
      <main className="content">
        <Outlet />
      </main>
    </div>
  );
};

export default WorkerDashboard;
