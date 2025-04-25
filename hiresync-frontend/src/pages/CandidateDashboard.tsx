import React, { useState } from 'react';
import '../css/CandidateDashboard.css';
import JobListings from './JobListings';
import ApplicationStatus from './ApplicationStatus';

const CandidateDashboard = () => {
  const [activeTab, setActiveTab] = useState("jobs");

  const renderTab = () => {
    switch (activeTab) {
      case "jobs":
        return <JobListings />;
      case "status":
        return <ApplicationStatus />;
      default:
        return null;
    }
  };

  return (
    <div className="candidate-dashboard">
      <aside className="sidebar">
        <h2>Aday Paneli</h2>
        <ul>
          <li onClick={() => setActiveTab("jobs")}>📌 İş İlanları</li>
          <li onClick={() => setActiveTab("status")}>📄 Başvuru Durumu</li>
        </ul>
      </aside>
      <main className="content">{renderTab()}</main>
    </div>
  );
};

export default CandidateDashboard;
