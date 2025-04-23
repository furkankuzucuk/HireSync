import React from 'react';
import '../css/WorkerTraining.css';

const WorkerTraining = () => {
  return (
    <div className="worker-training">
      <h2>📚 Eğitimler ve Sınavlar</h2>
      <table>
        <thead>
          <tr>
            <th>Eğitim / Sınav</th>
            <th>Tarih</th>
            <th>Katılım</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Python Programlama Eğitimi</td>
            <td>10 Mart 2024</td>
            <td><button className="btn green">Katıl</button></td>
          </tr>
          <tr>
            <td>Yazılım Test Sınavı</td>
            <td>15 Mart 2024</td>
            <td><button className="btn green">Katıl</button></td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default WorkerTraining;
