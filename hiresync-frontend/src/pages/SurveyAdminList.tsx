import React, { useEffect, useState } from 'react';
import { getSurveysForUser } from '../services/surveyService';
import { useNavigate } from 'react-router-dom';

const SurveyAdminList = () => {
    const [surveys, setSurveys] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        getSurveysForUser().then(setSurveys);
    }, []);

    return (
        <div>
            <h2>Anketler</h2>
            <ul>
                {surveys.map((survey: any) => (
                    <li key={survey.satisfactionSurveyId}>
                        {survey.surveyTitle}
                        <button onClick={() => navigate(`/admin-dashboard/survey-results/${survey.satisfactionSurveyId}`)}>
                            Sonuçları Göster
                            </button>
                             <button
                            onClick={() =>
                            navigate(`/admin-dashboard/survey-edit/${survey.satisfactionSurveyId}`)
                            }
                            style={{ marginLeft: '10px', backgroundColor: '#f39c12', color: 'white' }}
                        >
                            Güncelle
                        </button>
                    </li>
                ))}
            </ul>
             <button
                onClick={() => navigate('/admin-dashboard/survey-create')}
                style={{
                marginTop: '20px',
                padding: '10px 20px',
                backgroundColor: '#2c3e50',
                color: '#fff',
                border: 'none',
                borderRadius: '5px',
                }}
            >
                ➕ Yeni Anket Oluştur
            </button>
        </div>
    );
};

export default SurveyAdminList;
