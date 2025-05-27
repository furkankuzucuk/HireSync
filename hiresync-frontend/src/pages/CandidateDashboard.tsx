import React from "react";
import { NavLink, Outlet } from "react-router-dom";
import useLogout from "./useLogout";
import "../css/CandidateDashboard.css";

const CandidateDashboard: React.FC = () => {
  const logout = useLogout();
  const username = localStorage.getItem("username") || "Bilinmiyor";

  return (
    <div className="dashboard-wrapper d-flex">
      {/* Sidebar */}
      <aside className="dashboard-sidebar bg-dark text-white p-4 d-flex flex-column">
        <div className="text-center mb-4">
          <h4></h4>
          <p className="small">👤 {username}</p>
        </div>

        <nav className="nav flex-column flex-grow-1 gap-2">
          <NavLink
            to="/candidate-dashboard"
            end
            className={({ isActive }) =>
              `nav-link sidebar-link ${isActive ? "active" : ""}`
            }
          >
            📌 İş İlanları
          </NavLink>

          <NavLink
            to="/candidate-dashboard/status"
            className={({ isActive }) =>
              `nav-link sidebar-link ${isActive ? "active" : ""}`
            }
          >
            📄 Başvuru Durumu
          </NavLink>

          <NavLink
            to="/candidate-dashboard/upload-resume"
            className={({ isActive }) =>
              `nav-link sidebar-link ${isActive ? "active" : ""}`
            }
          >
            📤 CV Yükle
          </NavLink>
        </nav>

        <button onClick={logout} className="btn btn-danger mt-4 w-100">
          🚪 Çıkış Yap
        </button>
      </aside>

      {/* Main Content */}
      <main className="dashboard-content bg-light p-4 flex-grow-1">
        <Outlet />
      </main>
    </div>
  );
};

export default CandidateDashboard;
