import React, { useEffect, useState } from 'react';
import { getAllSurveys } from '../services/SurveyService';
import { useNavigate, useLocation } from 'react-router-dom';
import '../css/SurveyAdminList.css';

const SurveyAdminList = () => {
  const [surveys, setSurveys] = useState([]);
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    getAllSurveys().then(setSurveys);
  }, []);

  // Sadece survey-results sayfasındayken butonu gizle
  const isOnResultsPage = location.pathname.includes('/admin-dashboard/survey-results');

  return (
    <div className="survey-admin-container">
      <div className="header">
        <h2 className="text-center mb-4">📋 Anket Yönetimi</h2>

        {!isOnResultsPage && (
          <button
            onClick={() => navigate('/admin-dashboard/survey-create')}
            className="btn btn-dark"
          >
            ➕ Yeni Anket Oluştur
          </button>
        )}
      </div>

      {surveys.length === 0 ? (
        <p>Hiç anket bulunmamaktadır.</p>
      ) : (
        <div className="survey-list">
          {surveys.map((survey: any) => (
            <div key={survey.satisfactionSurveyId} className="survey-card shadow-sm">
              <h5 className="mb-2">{survey.surveyTitle}</h5>
              <div className="btn-group">
                <button
                  className="btn btn-primary"
                  onClick={() =>
                    navigate(`/admin-dashboard/survey-results/${survey.satisfactionSurveyId}`)
                  }
                >
                  Sonuçları Gör
                </button>
                <button
                  className="btn btn-warning"
                  onClick={() =>
                    navigate(`/admin-dashboard/survey-edit/${survey.satisfactionSurveyId}`)
                  }
                >
                  Güncelle
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default SurveyAdminList;
