import React, { useEffect, useState } from 'react';
import { getAllSurveys } from '../services/SurveyService';
import { useNavigate } from 'react-router-dom';
import '../css/SurveyList.css';

const SurveyList = () => {
  const [surveys, setSurveys] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    getAllSurveys()
      .then(setSurveys)
      .catch(err => {
        console.error("Anketler alınırken hata oluştu:", err);
        setSurveys([]);
      });
  }, []);

  return (
    <div className="survey-list-container">
      <h2 className="text-center mb-4">🗳️ Tüm Anketler</h2>
      {surveys.length === 0 ? (
        <p className="text-center text-muted">Henüz anket bulunmamaktadır.</p>
      ) : (
        <ul className="survey-list">
          {surveys.map((survey: any) => (
            <li key={survey.satisfactionSurveyId} className="survey-item">
              <span className="survey-title">{survey.surveyTitle}</span>
              <button
                className="btn btn-primary btn-sm"
                onClick={() => navigate(`/worker-dashboard/surveys/${survey.satisfactionSurveyId}`)}
              >
                Katıl
              </button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default SurveyList;
