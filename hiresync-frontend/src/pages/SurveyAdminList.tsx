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
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default SurveyAdminList;
