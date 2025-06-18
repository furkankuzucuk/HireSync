import React from "react";
import { Outlet, Link } from "react-router-dom";
import useLogout from "./useLogout";
import "../css/AdminDashboard.css";

const AdminDashboard = () => {
  const logout = useLogout();
  const username = localStorage.getItem("username");

  return (
    <div className="admin-layout d-flex">
      {/* Sidebar */}
      <aside className="sidebar bg-dark text-white">
        <h4 className="text-center mb-3"></h4>
        <p className="text-center">
          Hoş Geldiniz
        </p>
        <ul className="nav flex-column mt-4">
          <li className="nav-item">
            <Link className="nav-link" to="/admin-dashboard">🏠 Ana Sayfa</Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link" to="/admin-dashboard/jobs">📌 İş İlanları</Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link" to="/admin-dashboard/joblist">📋 Yayınlanan İlanlar</Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link" to="/admin-dashboard/jobapplications">📂 Başvurular</Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link" to="/admin-dashboard/exams">📝 Sınavlar</Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link" to="/admin-dashboard/performance">📊 Performans</Link>
          </li>
          
          <li className="nav-item">
            <Link className="nav-link" to="/admin-dashboard/survey-results">📋 Anket Sonuçları</Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link" to="/admin-dashboard/survey-create">➕ Anket Oluştur</Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link" to="/admin-dashboard/leaves">📅 İzin Talepleri</Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link" to="/admin-dashboard/announcements">📢 Duyurular</Link>
          </li>
        </ul>
        <button onClick={logout} className="btn btn-danger w-100 mt-4">🚪 Çıkış Yap</button>
      </aside>

      {/* Content */}
      <main className="content-area">
        <Outlet />
      </main>
    </div>
  );
};

export default AdminDashboard;
