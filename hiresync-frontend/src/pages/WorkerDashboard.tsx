import React from "react";
import { Outlet, Link } from "react-router-dom";
import useLogout from "./useLogout";
import "../css/WorkerDashboard.css";

const WorkerDashboard = () => {
  const logout = useLogout();
  const username = localStorage.getItem('username');

  return (
    <div className="container-fluid">
      <div className="row">
        {/* Sidebar */}
        <nav className="col-md-3 col-lg-2 d-md-block bg-primary sidebar position-fixed h-100 text-white p-3">
          <h4 className="text-white">Çalışan Paneli</h4>
          <p className="mt-3">Hoş Geldin, {username} 👋</p>
          <ul className="nav flex-column mt-4">
            <li className="nav-item">
              <Link className="nav-link text-white" to="/worker-dashboard">🏠 Ana Sayfa</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link text-white" to="/worker-dashboard/leave">📅 İzin Talebi</Link>
            </li>
            <li className="nav-item">
            <Link className="nav-link text-white" to="/worker-dashboard/leave-history">📄 İzin Geçmişim</Link>
            </li>

            <li className="nav-item">
              <Link className="nav-link text-white" to="/worker-dashboard/training">📚 Eğitimler ve Sınavlar</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link text-white" to="/worker-dashboard/exam-results">📊 Sınav Sonuçlarım</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link text-white" to="/worker-dashboard/surveys">📝 Memnuniyet Anketleri</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link text-white" to="/worker-dashboard/announcements">📢 Duyurular</Link>
            </li>
            <li className="nav-item mt-4">
              <button onClick={logout} className="btn btn-danger w-100">🚪 Çıkış Yap</button>
            </li>
          </ul>
        </nav>

        {/* Content */}
        <main className="col-md-9 ms-sm-auto col-lg-10 px-md-4 offset-md-3 offset-lg-2 py-4">
          <div className="tab-content bg-white p-4 rounded shadow-sm">
            <Outlet />
          </div>
        </main>
      </div>
    </div>
  );
};

export default WorkerDashboard;
