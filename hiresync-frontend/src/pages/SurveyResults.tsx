import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import '../css/SurveyResults.css';

interface SurveyQuestionResultDto {
  questionText: string;
  answerDistribution: Record<string, number>;
}

const SurveyResults = () => {
  const { id } = useParams();
  const [results, setResults] = useState<SurveyQuestionResultDto[]>([]);

  useEffect(() => {
    if (!id) return;

    fetch(`/api/surveyquestions/results/${id}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
        'Content-Type': 'application/json',
      },
    })
      .then(res => res.json())
      .then(data => {
        if (Array.isArray(data)) {
          setResults(data);
        } else {
          console.error('Beklenmeyen format:', data);
          setResults([]);
        }
      })
      .catch(err => {
        console.error('Sonuçlar alınamadı:', err);
        alert('Bir hata oluştu.');
      });
  }, [id]);

  return (
    <div className="survey-results-container">
      <h2 className="text-center mb-4">📊 Anket Sonuçları</h2>
      {results.length === 0 ? (
        <p className="text-muted">Henüz sonuç bulunmamaktadır.</p>
      ) : (
        results.map((res, index) => {
          const totalVotes = Object.values(res.answerDistribution).reduce((a, b) => a + b, 0);

          return (
            <div key={index} className="survey-question-card shadow-sm">
              <h5>{res.questionText}</h5>
              <ul className="answer-list">
                {Object.entries(res.answerDistribution).map(([answer, count]) => {
                  const percentage = totalVotes > 0 ? (count / totalVotes) * 100 : 0;

                  return (
                    <li key={answer} className="answer-item">
                      <span className="answer-label">{answer}</span>
                      <div className="progress-bar-wrapper">
                        <div
                          className="progress-bar"
                          style={{ width: `${percentage}%` }}
                        >
                          {count} oy ({percentage.toFixed(1)}%)
                        </div>
                      </div>
                    </li>
                  );
                })}
              </ul>
            </div>
          );
        })
      )}
    </div>
  );
};

export default SurveyResults;
