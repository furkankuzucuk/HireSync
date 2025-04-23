import React from "react";
import "../css/JobPostingManagement.css";

const JobPostingManagement = () => {
  return (
    <div className="tab-content">
      <h2>📌 İş İlanları Yönetimi</h2>
      <form>
        <label>Pozisyon:</label>
        <input type="text" placeholder="Pozisyon adı" />
        <label>Açıklama:</label>
        <textarea placeholder="İlan açıklaması" />
        <button className="btn dark">İlanı Yayınla</button>
      </form>
      <h3>Gelen Başvurular</h3>
      <table>
        <thead>
          <tr>
            <th>Ad Soyad</th>
            <th>Email</th>
            <th>CV</th>
            <th>İşlem</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Ali Yılmaz</td>
            <td>ali@example.com</td>
            <td><a href="#">Görüntüle</a></td>
            <td><button>Onayla</button></td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default JobPostingManagement;
