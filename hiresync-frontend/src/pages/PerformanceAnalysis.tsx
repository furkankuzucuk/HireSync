// Gerekli chart.js bileşenleri
import React, { useEffect, useState } from 'react';
import axios from '../services/axiosInstance';
import { Bar } from 'react-chartjs-2';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Tooltip,
  Legend,
  ChartOptions
} from 'chart.js';

import "../css/PerformanceAnalysis.css";

ChartJS.register(CategoryScale, LinearScale, BarElement, Tooltip, Legend);

interface UserExamResult {
  userExamId: number;
  userId: number;
  examId: number;
  examName: string;
  userName: string;
  lastName: string;
  score: number;
}

const PerformanceAnalysis = () => {
  const [results, setResults] = useState<UserExamResult[]>([]);
  const [users, setUsers] = useState<any[]>([]);
  const [exams, setExams] = useState<any[]>([]);
  const [selectedUser, setSelectedUser] = useState<number | null>(null);
  const [selectedExam, setSelectedExam] = useState<number | null>(null);
  const [showCharts, setShowCharts] = useState(false);

  useEffect(() => {
    axios.get("/api/users").then(res => setUsers(res.data.filter((u: any) => u.roleName === "Worker")));
    axios.get("/api/exams").then(res => setExams(res.data));
  }, []);

  const fetchResults = async () => {
    try {
      const params: any = {};
      if (selectedUser !== null) params.userId = selectedUser;
      if (selectedExam !== null) params.examId = selectedExam;

      const res = await axios.get("/api/userexams/filter", { params });
      setResults(res.data);
      setShowCharts(true);
    } catch (error) {
      console.error("Veri alınamadı:", error);
      setResults([]);
      setShowCharts(false);
    }
  };

  const scores = results.map(r => r.score);
  const average = scores.length ? (scores.reduce((a, b) => a + b, 0) / scores.length).toFixed(1) : 0;
  const maxScore = scores.length ? Math.max(...scores) : 0;
  const minScore = scores.length ? Math.min(...scores) : 0;

  const warmColors = ['#FF6B6B', '#FFA94D', '#FFD43B', '#FAB005', '#FF922B', '#FF8A65', '#FF7043', '#FF5722', '#FF9800', '#FFC107'];

  const barData = {
    labels: results.map(r => r.examName),
    datasets: [
      {
        label: 'Puanlar',
        data: results.map(r => r.score),
        backgroundColor: results.map((_, i) => warmColors[i % warmColors.length]),
        borderColor: '#444',
        borderWidth: 1
      }
    ]
  };

  const barOptions: ChartOptions<'bar'> = {
    responsive: true,
    plugins: {
      legend: { display: false },
      title: {
        display: true,
        text: `📊 Ortalama: ${average} | 🏆 En Yüksek: ${maxScore} | 🧨 En Düşük: ${minScore}`,
        font: { size: 16 }
      },
      tooltip: {
        callbacks: {
          title: (tooltipItems) => results[tooltipItems[0].dataIndex].examName,
          label: (tooltipItem) => {
            const result = results[tooltipItem.dataIndex];
            return `${result.userName} ${result.lastName} - ${result.score} puan`;
          }
        }
      }
    },
    scales: {
      x: {
        ticks: {
          maxRotation: 45,
          minRotation: 45,
          font: {
            size: 12
          }
        }
      },
      y: {
        beginAtZero: true,
        max: 100
      }
    }
  };

  return (
    <div className="performance-analysis container mt-4">
      <h2 className="mb-4">📊 Performans Analizi</h2>

      <div className="filters mb-4 d-flex gap-3">
        <select className="form-select" onChange={(e) => setSelectedUser(e.target.value ? Number(e.target.value) : null)}>
          <option value="">Tüm Kullanıcılar</option>
          {users.map((u) => (
            <option key={u.userId} value={u.userId}>{u.name} {u.lastName}</option>
          ))}
        </select>

        <select className="form-select" onChange={(e) => setSelectedExam(e.target.value ? Number(e.target.value) : null)}>
          <option value="">Tüm Sınavlar</option>
          {exams.map((e) => (
            <option key={e.examId} value={e.examId}>{e.examName}</option>
          ))}
        </select>
      </div>

      <button
        className="btn btn-primary mb-4"
        onClick={fetchResults}
      >
        📈 Değerlendirmeyi Gör
      </button>

      {showCharts && results.length > 0 && (
        <div className="mb-5">
          <Bar data={barData} options={barOptions} />
        </div>
      )}

      {showCharts && results.length === 0 && (
        <div className="text-center text-muted">Uygun veri bulunamadı.</div>
      )}
    </div>
  );
};

export default PerformanceAnalysis;