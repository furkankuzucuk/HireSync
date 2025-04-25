import React from 'react';
import '../css/ApplicationStatus.css';

const ApplicationStatus = () => {
  return (
    <div className="application-status">
      <h2>📄 Başvuru Durumlarım</h2>
      <table>
        <thead>
          <tr>
            <th>Pozisyon</th>
            <th>Departman</th>
            <th>Durum</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Yazılım Mühendisi</td>
            <td>Bilgi Teknolojileri</td>
            <td>Başvuru Değerlendiriliyor</td>
          </tr>
          <tr>
            <td>İnsan Kaynakları Uzmanı</td>
            <td>İK</td>
            <td>Reddedildi</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default ApplicationStatus;
