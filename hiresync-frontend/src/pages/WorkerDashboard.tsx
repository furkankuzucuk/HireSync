import React, { useState } from 'react';
import WorkerAnnouncements from './WorkerAnnouncements';
import WorkerLeaveRequest from './WorkerLeaveRequest';
import WorkerSurveys from './WorkerSurveys';
import WorkerTraining from './WorkerTraining';
import '../css/WorkerDashboard.css';

const WorkerDashboard = () => {
  const [activeTab, setActiveTab] = useState('dashboard');

  const renderTab = () => {
    switch (activeTab) {
      case 'dashboard':
        return (
          <div className="tab-content">
            <h2>🏠 Hoş Geldiniz, Çalışan</h2>
            <p>Bugün: {new Date().toLocaleDateString("tr-TR")}</p>
            <div className="dashboard-grid">
              <div className="card">📅 Bekleyen İzin Talepleri: 1</div>
              <div className="card">📚 Aktif Eğitim: 2</div>
              <div className="card">📝 Anketler: 3</div>
              <div className="card">📢 Yeni Duyurular: 2</div>
            </div>
            <h3>📢 Son Duyurular</h3>
            <ul>
              <li>15 Mart 2024: Yeni çalışma saatleri güncellendi</li>
              <li>10 Mart 2024: Şirket içi eğitim programı açıklandı</li>
            </ul>
          </div>
        );
      case 'announcements':
        return <WorkerAnnouncements />;
      case 'leave':
        return <WorkerLeaveRequest />;
      case 'surveys':
        return <WorkerSurveys />;
      case 'training':
        return <WorkerTraining />;
      default:
        return null;
    }
  };

  return (
    <div className="worker-dashboard">
      <aside className="sidebar">
        <h2>Çalışan Paneli</h2>
        <ul>
          <li onClick={() => setActiveTab('dashboard')}>🏠 Ana Sayfa</li>
          <li onClick={() => setActiveTab('leave')}>📅 İzin Talebi</li>
          <li onClick={() => setActiveTab('training')}>📚 Eğitimler ve Sınavlar</li>
          <li onClick={() => setActiveTab('surveys')}>📝 Memnuniyet Anketleri</li>
          <li onClick={() => setActiveTab('announcements')}>📢 Duyurular</li>
        </ul>
      </aside>
      <main className="content">
        {renderTab()}
      </main>
    </div>
  );
};

export default WorkerDashboard;
