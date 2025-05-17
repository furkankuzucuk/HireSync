import React, { useEffect, useState } from 'react';
import axios from '../services/axiosInstance';
import { useSearchParams } from 'react-router-dom';
import { Line } from 'react-chartjs-2';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
} from 'chart.js';

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend);

interface PerformanceReviewDto {
  performanceReviewId: number;
  userExamId: number;
  averageScore: number;
  performanceRate: number;
  reviewSummary: string;
  reviewDate: string;
}

const PerformanceReviewPage = () => {
  const [searchParams] = useSearchParams();
  const [reviews, setReviews] = useState<PerformanceReviewDto[]>([]);

  useEffect(() => {
    const userId = searchParams.get("userId");
    if (userId) {
      axios
        .get(`/api/performancereviews/user/${userId}`)
        .then(res => setReviews(res.data))
        .catch(err => console.error("Veri alınamadı:", err));
    }
  }, [searchParams]);

  const chartData = {
    labels: reviews.map(r => new Date(r.reviewDate).toLocaleDateString()),
    datasets: [
      {
        label: 'Ortalama Puan',
        data: reviews.map(r => r.averageScore),
        borderColor: 'blue',
        backgroundColor: 'rgba(0, 0, 255, 0.1)',
        tension: 0.3
      },
      {
        label: 'Performans Puanı (1-5)',
        data: reviews.map(r => r.performanceRate),
        borderColor: 'orange',
        backgroundColor: 'rgba(255,165,0, 0.1)',
        tension: 0.3,
        yAxisID: 'y1'
      }
    ]
  };

  const chartOptions = {
    responsive: true,
    plugins: {
      legend: { position: 'top' as const },
      title: { display: true, text: 'Performans Gelişimi' }
    },
    scales: {
      y: {
        beginAtZero: true,
        title: { display: true, text: 'Ortalama Puan' }
      },
      y1: {
        beginAtZero: true,
        position: 'right' as const,
        title: { display: true, text: 'Performans Puanı (1-5)' },
        ticks: { stepSize: 1, max: 5 }
      }
    }
  };

  return (
    <div>
      <h2>Kullanıcı Performans Grafiği</h2>
      {reviews.length === 0 ? (
        <p>Performans verisi bulunamadı.</p>
      ) : (
        <Line data={chartData} options={chartOptions} />
      )}
    </div>
  );
};

export default PerformanceReviewPage;
