import React from "react";
import "../css/OnlineExam.css";

const OnlineExam = () => {
  return (
    <div className="tab-content">
      <h2>📝 Online Sınav Yönetimi</h2>
      <form>
        <label>Sınav Başlığı:</label>
        <input type="text" placeholder="Sınav adı" />
        <label>Soru:</label>
        <textarea placeholder="Sınav sorusu" />
        <button className="btn green">Sınav Oluştur</button>
      </form>
      <h3>Mevcut Sınavlar</h3>
      <ul>
        <li>React Testi - 5 Katılımcı</li>
      </ul>
    </div>
  );
};

export default OnlineExam;
