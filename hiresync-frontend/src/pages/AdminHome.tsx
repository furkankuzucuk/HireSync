import React from 'react';

const AdminHome = () => {
  return (
    <div className="tab-content">
      <h2>🏠 Hoş Geldiniz, Yönetici</h2>
      <p>Bugün: {new Date().toLocaleDateString("tr-TR")}</p>
      <p>Yönetim panelinden işlemlerinizi gerçekleştirebilirsiniz.</p>
    </div>
  );
};

export default AdminHome;
