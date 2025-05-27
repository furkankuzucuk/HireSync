import React, { useEffect, useState } from "react";
import axios from "axios";
import "../css/LeaveRequests.css";

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
    <div className="leave-requests container mt-4">
      <div className="card p-4 shadow">
        <h2 className="text-center mb-4 text-primary">📅 İzin Talepleri</h2>
        <div className="table-responsive">
          <table className="table table-striped align-middle">
            <thead className="table-dark">
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
                      className="btn btn-success btn-sm me-2"
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
      </div>
    </div>
  );
};

export default LeaveRequests;
