import React from 'react';

const CandidateHome = () => {
  return (
    <div className="tab-content">
      <h2>📌 Hoş Geldiniz, Aday</h2>
      <p>Bugün: {new Date().toLocaleDateString("tr-TR")}</p>
      <p>Buradan açık pozisyonlara göz atabilir veya başvurularınızı takip edebilirsiniz.</p>
    </div>
  );
};

export default CandidateHome;
