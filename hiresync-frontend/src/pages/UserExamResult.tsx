import React, { useEffect, useState } from 'react';
import axios from '../services/axiosInstance';

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
    <div>
      <h2>Geçmiş Sınav Sonuçlarınız</h2>
      <table>
        <thead>
          <tr>
            <th>Sınav Adı</th>
            <th>Puan</th>
          </tr>
        </thead>
        <tbody>
          {results.map(r => (
            <tr key={r.userExamId}>
              <td>{r.examName}</td>
              <td>{r.score}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UserExamResults;
