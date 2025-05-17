import React, { useEffect, useState } from 'react';
import axios from '../services/axiosInstance';
import { useNavigate } from 'react-router-dom';

const PerformanceAnalysis = () => {
  const [users, setUsers] = useState([]);
  const [exams, setExams] = useState([]);
  const [selectedUser, setSelectedUser] = useState<number | null>(null);
  const [selectedExam, setSelectedExam] = useState<number | null>(null);
  const [results, setResults] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    axios.get('/api/users').then(res => setUsers(res.data));
    axios.get('/api/exams').then(res => setExams(res.data));
  }, []);

  useEffect(() => {
    const params: any = {};
    if (selectedUser) params.userId = selectedUser;
    if (selectedExam) params.examId = selectedExam;

    axios
      .get('/api/userexams/filter', { params })
      .then(res => setResults(res.data))
      .catch(err => console.error('Filter error', err));
  }, [selectedUser, selectedExam]);

  return (
    <div>
      <h2>Performans Analizi</h2>

      <div style={{ display: 'flex', gap: '1rem' }}>
        <select onChange={e => setSelectedUser(Number(e.target.value) || null)}>
          <option value="">Tüm Kullanıcılar</option>
          {users.map((u: any) => (
            <option key={u.userId} value={u.userId}>
              {u.name} {u.lastName}
            </option>
          ))}
        </select>

        <select onChange={e => setSelectedExam(Number(e.target.value) || null)}>
          <option value="">Tüm Sınavlar</option>
          {exams.map((e: any) => (
            <option key={e.examId} value={e.examId}>
              {e.examName}
            </option>
          ))}
        </select>
      </div>

      <table>
        <thead>
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

      <button
        disabled={!selectedUser}
        onClick={async () => {
          try {
            // önce performans oluştur
            await axios.post(`/api/performancereviews/generate-review/${selectedUser}`);
            // ardından yönlendir
            navigate(`/admin-dashboard/performance-review?userId=${selectedUser}`);
          } catch (error) {
            console.error("Performans oluşturulamadı", error);
            alert("Performans değerlendirmesi oluşturulamadı.");
          }
        }}
      >
        Değerlendirmeyi Gör
    </button>

    </div>
  );
};

export default PerformanceAnalysis;
