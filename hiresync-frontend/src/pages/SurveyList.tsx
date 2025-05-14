import React, { useEffect, useState } from 'react';
import { getSurveysForUser } from '../services/surveyService';
import { useNavigate } from 'react-router-dom';

const SurveyList = () => {
    const [surveys, setSurveys] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        getSurveysForUser().then(setSurveys);
    }, []);

    return (
        <div>
            <h2>Departmanınıza Ait Anketler</h2>
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
