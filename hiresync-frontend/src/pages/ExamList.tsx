import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { ExamDto } from '../types/ExamTypes';
import "../css/ExamList.css";

const ExamList = () => {
  const [exams, setExams] = useState<ExamDto[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    axios.get('/api/exams')
      .then(response => setExams(response.data))
      .catch(error => console.error('Error fetching exams:', error));
  }, []);

  const handleStartExam = (id: number) => {
    navigate(`/worker-dashboard/exam/${id}`);
  };

  return (
    <div className="exam-list container mt-4">
      <div className="exam-card shadow-sm p-4 bg-white rounded">
        <h2 className="mb-4 text-primary">📝 Online Sınavlar</h2>
        {exams.length === 0 ? (
          <p>Henüz sınav bulunmamaktadır.</p>
        ) : (
          <ul className="list-group">
            {exams.map(exam => (
              <li key={exam.examId} className="list-group-item d-flex justify-content-between align-items-center">
                <div>
                  <strong>{exam.examName}</strong> - {new Date(exam.examDate).toLocaleDateString("tr-TR")}
                </div>
                <button className="btn btn-outline-success btn-sm" onClick={() => handleStartExam(exam.examId)}>
                  Sınava Başla
                </button>
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
};

export default ExamList;
