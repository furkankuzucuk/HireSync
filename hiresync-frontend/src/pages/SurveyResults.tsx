import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

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
      'Content-Type': 'application/json'
    }
  })
    .then(res => res.json())
    .then(data => {
      console.log("Gelen sonuçlar:", data);
      if (Array.isArray(data)) {
        setResults(data);
      } else {
        console.error("Beklenmeyen format:", data);
        setResults([]);
      }
    })
    .catch(err => {
      console.error("Sonuçlar alınamadı:", err);
      alert("Bir hata oluştu.");
    });
}, [id]);


  return (
    <div>
      <h2>Anket Sonuçları</h2>
      {results.map((res, index) => (
        <div key={index} style={{ marginBottom: '20px' }}>
          <h4>{res.questionText}</h4>
          <ul>
            {Object.entries(res.answerDistribution).map(([answer, count]) => (
              <li key={answer}>
                {answer}: {count} oy
              </li>
            ))}
          </ul>
        </div>
      ))}
    </div>
  );
};

export default SurveyResults;
