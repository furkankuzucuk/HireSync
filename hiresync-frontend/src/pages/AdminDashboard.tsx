// src/pages/AdminDashboard.tsx
import React from "react";
import { Outlet, Link } from "react-router-dom";
import useLogout from "./useLogout";
import "../css/AdminDashboard.css";

const AdminDashboard = () => {
  const logout = useLogout();
  const username = localStorage.getItem("username");

  return (
    <div className="admin-layout d-flex">
      <div className="sidebar bg-dark text-white p-3 vh-100" style={{ width: "250px" }}>
        <h4 className="text-center mb-4">Yönetici Paneli</h4>
        <p className="text-center">Hoş Geldin, <strong>{username}</strong> 👋</p>
        <ul className="nav flex-column mt-4">
          <li className="nav-item"><Link className="nav-link text-white" to="/admin-dashboard">🏠 Ana Sayfa</Link></li>
          <li className="nav-item"><Link className="nav-link text-white" to="/admin-dashboard/jobs">📌 İş İlanları Yönetimi</Link></li>
          <li className="nav-item"><Link className="nav-link text-white" to="/admin-dashboard/joblist">📋 Yayınlanan İlanlar</Link></li>
          <li className="nav-item"><Link className="nav-link text-white" to="/admin-dashboard/exams">📝 Online Sınavlar</Link></li>
          <li className="nav-item"><Link className="nav-link text-white" to="/admin-dashboard/performance">📊 Performans Analizi</Link></li>
          <li className="nav-item"><Link className="nav-link text-white" to="/admin-dashboard/survey-results">📋 Anket Sonuçları</Link></li>
          <li className="nav-item"><Link className="nav-link text-white" to="/admin-dashboard/leaves">📅 İzin Talepleri</Link></li>
        </ul>
        <button onClick={logout} className="btn btn-danger w-100 mt-4">🚪 Çıkış Yap</button>
      </div>

      <div className="content-area flex-grow-1 p-4" style={{ backgroundColor: "#f8f9fa", minHeight: "100vh" }}>
        <Outlet />
      </div>
    </div>
  );
};

export default AdminDashboard;
