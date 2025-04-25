import React, { useState } from "react";
import "../css/AdminDashboard.css";
import JobPostingManagement from "./JobPostingManagement";
import OnlineExam from "./OnlineExam";
import PerformanceAnalysis from "./PerformanceAnalysis";
import LeaveRequests from "./LeaveRequests";

const AdminDashboard = () => {
  const [activeTab, setActiveTab] = useState("dashboard");

  const renderTab = () => {
    switch (activeTab) {
      case "dashboard":
        return (
          <div className="tab-content">
            <h2>🏠 Hoş Geldiniz, Yönetici</h2>
            <p>Bugün: {new Date().toLocaleDateString("tr-TR")}</p>
            {/* Dashboard kartları ve duyurular buraya */}
          </div>
        );
      case "jobs":
        return <JobPostingManagement />;
      case "exams":
        return <OnlineExam />;
      case "performance":
        return <PerformanceAnalysis />;
      case "leaves":
        return <LeaveRequests />;
      default:
        return null;
    }
  };

  return (
    <div className="admin-dashboard">
      <aside className="sidebar">
        <h2>Yönetici Paneli</h2>
        <ul>
          <li onClick={() => setActiveTab("dashboard")}>🏠 Ana Sayfa</li>
          <li onClick={() => setActiveTab("jobs")}>📌 İş İlanları</li>
          <li onClick={() => setActiveTab("exams")}>📝 Online Sınavlar</li>
          <li onClick={() => setActiveTab("performance")}>📊 Performans Analizi</li>
          <li onClick={() => setActiveTab("leaves")}>📅 İzin Talepleri</li>
        </ul>
      </aside>
      <main className="content">{renderTab()}</main>
    </div>
  );
};

export default AdminDashboard;
