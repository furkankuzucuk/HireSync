import React, { useEffect, useState } from 'react';
import { getAllSurveys } from '../services/SurveyService'; // yeni fonksiyon import edildi
import { useNavigate } from 'react-router-dom';

const SurveyList = () => {
    const [surveys, setSurveys] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        getAllSurveys().then(setSurveys).catch(err => {
          console.error(err);
          setSurveys([]);
        });
    }, []);

    return (
        <div>
            <h2>Tüm Anketler</h2>
            <ul>
                {surveys.map((survey: any) => (
                    <li key={survey.satisfactionSurveyId}>
                        {survey.surveyTitle}
                        <button onClick={() => navigate(`/worker-dashboard/surveys/${survey.satisfactionSurveyId}`)}>
                            Katıl
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default SurveyList;
