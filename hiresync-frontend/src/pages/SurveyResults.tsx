import React, { useEffect, useState } from 'react';
import { getSubmittedSurveys } from '../services/surveyAnswerService';

const SurveyResults = () => {
    const [results, setResults] = useState([]);

    useEffect(() => {
        getSubmittedSurveys().then(setResults);
    }, []);

    return (
        <div>
            <h2>Gönderilen Anketler</h2>
            <ul>
                {results.map((res: any) => (
                    <li key={res.surveyAnswerId}>
                        Soru ID: {res.surveyQuestionId} - Cevap: {res.answerText}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default SurveyResults;
