import React from "react";
import "../css/CandidateHome.css";

const CandidateHome = () => {
  const username = localStorage.getItem("username");

  return (
    <div className="candidate-home container">
      <div className="welcome-card shadow-sm">
        <h2 className="welcome-title">📌 Hoş Geldiniz, {username}</h2>
        <p><strong>Bugün:</strong> {new Date().toLocaleDateString("tr-TR")}</p>
        <p>
          Buradan açık pozisyonlara göz atabilir veya başvurularınızı takip edebilirsiniz.
        </p>
      </div>
    </div>
  );
};

export default CandidateHome;
