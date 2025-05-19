import React, { useEffect, useState } from "react";
import axios from "axios";

interface LeaveRequest {
  leaveRequestId: number;
  userName: string;
  leaveType: string;
  startDate: string;
  endDate: string;
  status: string;
  requestDate: string;
}

const LeaveRequests = () => {
  const [requests, setRequests] = useState<LeaveRequest[]>([]);

  useEffect(() => {
    axios.get("/api/leaverequests", {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`
      }
    })
    .then(res => setRequests(res.data))
    .catch(err => console.error("İzinler alınamadı", err));
  }, []);

  const updateStatus = async (id: number, newStatus: string) => {
    try {
      await axios.put(`/api/leaverequests/${id}`, { status: newStatus }, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`
        }
      });

      setRequests(prev =>
        prev.map(r =>
          r.leaveRequestId === id ? { ...r, status: newStatus } : r
        )
      );
    } catch (err) {
      alert("Güncelleme hatası");
      console.error(err);
    }
  };

  return (
    <div className="container mt-4">
      <h2>📅 İzin Talepleri</h2>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Ad Soyad</th>
            <th>İzin Türü</th>
            <th>Başlangıç</th>
            <th>Bitiş</th>
            <th>Durum</th>
            <th>İşlem</th>
          </tr>
        </thead>
        <tbody>
          {requests.map(req => (
            <tr key={req.leaveRequestId}>
              <td>{req.userName}</td>
              <td>{req.leaveType}</td>
              <td>{new Date(req.startDate).toLocaleDateString()}</td>
              <td>{new Date(req.endDate).toLocaleDateString()}</td>
              <td>{req.status}</td>
              <td>
                <button
                  className="btn btn-success btn-sm me-1"
                  onClick={() => updateStatus(req.leaveRequestId, "Onaylandı")}
                >
                  Onayla
                </button>
                <button
                  className="btn btn-danger btn-sm"
                  onClick={() => updateStatus(req.leaveRequestId, "Reddedildi")}
                >
                  Reddet
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default LeaveRequests;
