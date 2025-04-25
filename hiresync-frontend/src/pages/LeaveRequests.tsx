import React from "react";
import "../css/LeaveRequests.css";

const LeaveRequests = () => {
  return (
    <div className="tab-content">
      <h2>📅 İzin Talepleri</h2>
      <table>
        <thead>
          <tr>
            <th>Ad Soyad</th>
            <th>Tarih</th>
            <th>Sebep</th>
            <th>İşlem</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Zeynep Kaya</td>
            <td>12-14 Nisan</td>
            <td>Yıllık İzin</td>
            <td>
              <button>Onayla</button>
              <button>Reddet</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  );
};

export default LeaveRequests;
