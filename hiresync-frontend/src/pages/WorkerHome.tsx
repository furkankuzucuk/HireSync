import React from 'react';

const WorkerHome = () => {
  return (
    <div className="tab-content">
      <h2>🏠 Hoş Geldiniz, Çalışan</h2>
      <p>Bugün: {new Date().toLocaleDateString("tr-TR")}</p>
      <p>Buradan izin talebi, eğitimler ve duyurulara erişebilirsiniz.</p>
    </div>
  );
};

export default WorkerHome;
