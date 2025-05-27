import React, { useEffect, useState } from 'react';
import axios from '../services/axiosInstance';
import '../css/UserExamResults.css';

interface UserExamDto {
  userExamId: number;
  examId: number;
  examName: string;
  score: number;
}

const UserExamResults = () => {
  const [results, setResults] = useState<UserExamDto[]>([]);

  useEffect(() => {
    axios.get('/api/userexams/me')
      .then(res => setResults(res.data))
      .catch(err => console.error('User exam results error', err));
  }, []);

  return (
    <div className="exam-results-container">
      <div className="results-card shadow">
        <h2 className="text-center mb-4">🧪 Geçmiş Sınav Sonuçlarınız</h2>
        {results.length === 0 ? (
          <p className="text-center">Henüz sınav sonuçlarınız bulunmamaktadır.</p>
        ) : (
          <table className="table table-bordered table-striped">
            <thead className="table-dark">
              <tr>
                <th>Sınav Adı</th>
                <th>Puan</th>
              </tr>
            </thead>
            <tbody>
              {results.map((r) => (
                <tr key={r.userExamId}>
                  <td>{r.examName}</td>
                  <td>{r.score}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
};

export default UserExamResults;
