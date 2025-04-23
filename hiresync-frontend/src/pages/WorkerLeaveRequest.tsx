import React from 'react';
import '../css/WorkerLeaveRequest.css';

const WorkerLeaveRequest = () => {
  return (
    <div className="worker-leave-request">
      <h2>🗓️ İzin Talebi</h2>
      <form>
        <label>İzin Türü:</label>
        <select>
          <option>Yıllık İzin</option>
          <option>Sağlık İzni</option>
          <option>Diğer</option>
        </select>

        <label>Başlangıç Tarihi:</label>
        <input type="date" />

        <label>Bitiş Tarihi:</label>
        <input type="date" />

        <label>Açıklama:</label>
        <textarea placeholder="İzin sebebini giriniz" />

        <button type="submit" className="btn green">Talep Gönder</button>
      </form>
    </div>
  );
};

export default WorkerLeaveRequest;
