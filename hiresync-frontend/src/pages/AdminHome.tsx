import React from 'react';
import useLogout from "./useLogout";

const AdminHome = () => {
  const logout = useLogout();
  const username = localStorage.getItem("username");
  return (
  

    <div className="tab-content">
      <h2>🏠 Hoş Geldiniz, {username}</h2>
      <p>Bugün: {new Date().toLocaleDateString("tr-TR")}</p>
      <p>Yönetim panelinden işlemlerinizi gerçekleştirebilirsiniz.</p>
    </div>
  );
};

export default AdminHome;
