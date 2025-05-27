import React, { useState } from "react";
import axios from "axios";
import "../css/WorkerLeaveRequest.css";

const WorkerLeaveRequest = () => {
  const [leaveType, setLeaveType] = useState("Yıllık İzin");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [successMessage, setSuccessMessage] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    setSuccessMessage("");
    setErrorMessage("");

    try {
      await axios.post(
        "/api/leaverequests",
        {
          leaveType,
          startDate,
          endDate,
        },
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }
      );

      setSuccessMessage("✅ İzin talebiniz başarıyla gönderildi.");
      setStartDate("");
      setEndDate("");
    } catch (err) {
      console.error("Hata oluştu", err);
      setErrorMessage("❌ İzin talebi gönderilemedi.");
    }
  };

  return (
    <div className="worker-leave-request container mt-4">
      <h2>🗓️ İzin Talebi</h2>

      {successMessage && <div className="alert alert-success">{successMessage}</div>}
      {errorMessage && <div className="alert alert-danger">{errorMessage}</div>}

      <form onSubmit={handleSubmit}>
        <label>İzin Türü:</label>
        <select value={leaveType} onChange={(e) => setLeaveType(e.target.value)}>
          <option>Yıllık İzin</option>
          <option>Sağlık İzni</option>
          <option>Diğer</option>
        </select>

        <label>Başlangıç Tarihi:</label>
        <input
          type="date"
          value={startDate}
          onChange={(e) => setStartDate(e.target.value)}
          required
        />

        <label>Bitiş Tarihi:</label>
        <input
          type="date"
          value={endDate}
          onChange={(e) => setEndDate(e.target.value)}
          required
        />

        <button type="submit" className="btn btn-success mt-2">
          Talep Gönder
        </button>
      </form>
    </div>
  );
};

export default WorkerLeaveRequest;
