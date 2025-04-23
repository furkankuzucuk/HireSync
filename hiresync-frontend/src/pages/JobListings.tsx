import React from 'react';
import '../css/JobListings.css';

const JobListings = () => {
  return (
    <div className="job-listings">
      <h2>📌 Açık Pozisyonlar</h2>
      <table>
        <thead>
          <tr>
            <th>Pozisyon</th>
            <th>Departman</th>
            <th>İş Tanımı</th>
            <th>İşlem</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Yazılım Mühendisi</td>
            <td>Bilgi Teknolojileri</td>
            <td>Web ve mobil uygulamalar geliştirme</td>
            <td><button className="btn blue">Başvur</button></td>
          </tr>
          <tr>
            <td>İnsan Kaynakları Uzmanı</td>
            <td>İK</td>
            <td>Personel işe alım süreçlerini yönetme</td>
            <td><button className="btn blue">Başvur</button></td>
          </tr>
          <tr>
            <td>Satış Temsilcisi</td>
            <td>Satış & Pazarlama</td>
            <td>Müşteri portföyü geliştirme ve satış</td>
            <td><button className="btn blue">Başvur</button></td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default JobListings;
