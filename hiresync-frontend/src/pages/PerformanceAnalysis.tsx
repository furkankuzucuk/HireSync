import React from "react";
import "../css/PerformanceAnalysis.css";

const PerformanceAnalysis = () => {
  return (
    <div className="tab-content">
      <h2>📊 Performans Analizi</h2>
      <table>
        <thead>
          <tr>
            <th>Çalışan</th>
            <th>Sınav</th>
            <th>Sonuç</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Ahmet Yılmaz</td>
            <td>React Sınavı</td>
            <td>Başarılı</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default PerformanceAnalysis;
