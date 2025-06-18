// src/pages/MyLeaveRequests.tsx
import React, { useEffect, useState } from "react";
import axios from "axios";
import "../css/MyLeaveRequests.css";

interface LeaveRequest {
  leaveRequestId: number;
  leaveType: string;
  startDate: string;
  endDate: string;
  status: string;
  requestDate: string;
}

const MyLeaveRequests = () => {
  const [requests, setRequests] = useState<LeaveRequest[]>([]);

  useEffect(() => {
    const token = localStorage.getItem("token");

    axios
      .get("/api/leaverequests/user", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((res) => setRequests(res.data))
      .catch((err) => console.error("İzin geçmişi alınamadı", err));
  }, []);

  const getStatusBadge = (status: string) => {
    const map: Record<string, string> = {
      Onaylandı: "success",
      Reddedildi: "danger",
      Pending: "secondary",
    };
    return <span className={`badge bg-${map[status] || "light"}`}>{status}</span>;
  };

  return (
    <div className="my-leave-requests container mt-4">
      <h2 className="title">🧾 İzin Geçmişim</h2>
      <div className="table-wrapper">
        <table className="table table-bordered table-hover text-center align-middle">
          <thead className="table-light">
            <tr>
              <th>İzin Türü</th>
              <th>Başlangıç</th>
              <th>Bitiş</th>
              <th>Durum</th>
              <th>Başvuru Tarihi</th>
            </tr>
          </thead>
          <tbody>
            {requests.map((req) => (
              <tr key={req.leaveRequestId}>
                <td>{req.leaveType}</td>
                <td>{new Date(req.startDate).toLocaleDateString("tr-TR")}</td>
                <td>{new Date(req.endDate).toLocaleDateString("tr-TR")}</td>
                <td>{getStatusBadge(req.status)}</td>
                <td>{new Date(req.requestDate).toLocaleDateString("tr-TR")}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default MyLeaveRequests;
