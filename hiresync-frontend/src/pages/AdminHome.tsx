import React from 'react';
import useLogout from "./useLogout";
import "../css/AdminHome.css";

const AdminHome = () => {
  const logout = useLogout();
  const username = localStorage.getItem("username");

  return (
    <div className="admin-home container">
      <div className="welcome-box shadow-sm">
        <h2 className="welcome-title">🏠 Hoş Geldiniz, {username}</h2>
        <p className="text-muted">Bugün: {new Date().toLocaleDateString("tr-TR")}</p>
        <p className="lead">Yönetim panelinden işlemlerinizi gerçekleştirebilirsiniz.</p>
      </div>
    </div>
  );
};

export default AdminHome;
