import React from "react";
import { Outlet, Link } from "react-router-dom";
import "../css/CandidateDashboard.css";

const CandidateDashboard = () => {
  return (
    <div className="candidate-dashboard">
      <aside className="sidebar">
        <h2>Aday Paneli</h2>
        <ul>
          <li>
            <Link to="/candidate-dashboard">📌 İş İlanları</Link>
          </li>
          <li>
            <Link to="/candidate-dashboard/status">📄 Başvuru Durumu</Link>
          </li>
        </ul>
      </aside>
      <main className="content">
        <Outlet /> {/* Alt route componentlerini burada göstereceğiz */}
      </main>
    </div>
  );
};

export default CandidateDashboard;
