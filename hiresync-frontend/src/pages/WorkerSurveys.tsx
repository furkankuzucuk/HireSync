import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import '../css/WorkerSurveys.css';

interface Survey {
  satisfactionSurveyId: number;
  title: string;
  description: string;
  startDate: string;
  endDate: string;
}

const WorkerSurveys = () => {
  const [surveys, setSurveys] = useState<Survey[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchSurveys = async () => {
      try {
        const response = await axios.get('http://localhost:5065/api/satisfactionsurveys/user-department', {
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('authToken')}`
          }
        });
        setSurveys(response.data);
      } catch (error) {
        console.error('Error fetching surveys:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchSurveys();
  }, []);

  if (loading) return <div>Loading surveys...</div>;

  return (
    <div className="worker-surveys">
      <h2>Memnuniyet Anketleri</h2>
      {surveys.length === 0 ? (
        <p>Şu anda aktif anket bulunmamaktadır.</p>
      ) : (
        <div className="survey-list">
          {surveys.map((survey) => (
            <div key={survey.satisfactionSurveyId} className="survey-card">
              <h3>{survey.title}</h3>
              <p>{survey.description}</p>
              <div className="survey-dates">
                <span>Başlangıç: {new Date(survey.startDate).toLocaleDateString()}</span>
                <span>Bitiş: {new Date(survey.endDate).toLocaleDateString()}</span>
              </div>
              <Link 
                to={`/survey/${survey.satisfactionSurveyId}`} 
                className="take-survey-btn"
              >
                Ankete Katıl
              </Link>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default WorkerSurveys;