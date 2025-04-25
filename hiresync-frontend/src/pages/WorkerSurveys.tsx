import React from 'react';
import '../css/WorkerSurveys.css';

const WorkerSurveys = () => {
  return (
    <div className="worker-surveys">
      <h2>📝 Memnuniyet Anketleri</h2>
      <table>
        <thead>
          <tr>
            <th>Anket Adı</th>
            <th>Tarih</th>
            <th>İşlem</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Şirket Ortamı Değerlendirme</td>
            <td>05.03.2024</td>
            <td><button className="btn blue">Anketi Doldur</button></td>
          </tr>
          <tr>
            <td>Yönetim Memnuniyeti</td>
            <td>12.03.2024</td>
            <td><button className="btn blue">Anketi Doldur</button></td>
          </tr>
          <tr>
            <td>İş Yükü & Stres Yönetimi</td>
            <td>20.03.2024</td>
            <td><button className="btn blue">Anketi Doldur</button></td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default WorkerSurveys;
