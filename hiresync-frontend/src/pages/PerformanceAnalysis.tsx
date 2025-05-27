// src/pages/PerformanceAnalysis.tsx
import React, { useEffect, useState } from "react";
import axios from "../services/axiosInstance";
import { useNavigate } from "react-router-dom";
import "../css/PerformanceAnalysis.css";

const PerformanceAnalysis = () => {
  const [users, setUsers] = useState([]);
  const [exams, setExams] = useState([]);
  const [selectedUser, setSelectedUser] = useState<number | null>(null);
  const [selectedExam, setSelectedExam] = useState<number | null>(null);
  const [results, setResults] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    axios.get("/api/users").then((res) => {
      const onlyWorkers = res.data.filter((u: any) => u.roleName === "Worker");
      setUsers(onlyWorkers);
    });
    axios.get("/api/exams").then((res) => setExams(res.data));
  }, []);
  

  useEffect(() => {
    const params: any = {};
    if (selectedUser) params.userId = selectedUser;
    if (selectedExam) params.examId = selectedExam;

    axios
      .get("/api/userexams/filter", { params })
      .then((res) => setResults(res.data))
      .catch((err) => console.error("Filter error", err));
  }, [selectedUser, selectedExam]);

  return (
    <div className="performance-analysis container mt-4">
      <h2 className="mb-4">📊 Performans Analizi</h2>

      <div className="filters mb-4">
        <select
          className="form-select"
          onChange={(e) => setSelectedUser(Number(e.target.value) || null)}
        >
          <option value="">Tüm Kullanıcılar</option>
          {users.map((u: any) => (
            <option key={u.userId} value={u.userId}>
              {u.name} {u.lastName}
            </option>
          ))}
        </select>

        <select
          className="form-select"
          onChange={(e) => setSelectedExam(Number(e.target.value) || null)}
        >
          <option value="">Tüm Sınavlar</option>
          {exams.map((e: any) => (
            <option key={e.examId} value={e.examId}>
              {e.examName}
            </option>
          ))}
        </select>
      </div>

      <div className="table-responsive">
        <table className="table table-bordered table-striped">
          <thead className="table-dark">
            <tr>
              <th>Kullanıcı</th>
              <th>Sınav</th>
              <th>Puan</th>
            </tr>
          </thead>
          <tbody>
            {results.map((r: any) => (
              <tr key={r.userExamId}>
                <td>{r.userName}</td>
                <td>{r.examName}</td>
                <td>{r.score}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <button
        disabled={!selectedUser}
        className="btn btn-primary mt-3"
        onClick={async () => {
          try {
            await axios.post(`/api/performancereviews/generate-review/${selectedUser}`);
            navigate(`/admin-dashboard/performance-review?userId=${selectedUser}`);
          } catch (error) {
            console.error("Performans oluşturulamadı", error);
            alert("Performans değerlendirmesi oluşturulamadı.");
          }
        }}
      >
        📈 Değerlendirmeyi Gör
      </button>
    </div>
  );
};

export default PerformanceAnalysis;
