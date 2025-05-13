import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { submitSurveyAnswers } from '../services/surveyAnswerService';

const options = ["Kesinlikle Katılıyorum", "Katılıyorum", "Kararsızım", "Katılmıyorum", "Kesinlikle Katılmıyorum"];

const SurveyDetail = () => {
    const { id } = useParams();
    const [questions, setQuestions] = useState([]);
    const [answers, setAnswers] = useState<{ [key: number]: string }>({});

    useEffect(() => {
        axios.get(`/api/surveyquestions/survey/${id}`).then(res => {
            setQuestions(res.data);
        });
    }, [id]);

    const handleChange = (questionId: number, value: string) => {
        setAnswers(prev => ({ ...prev, [questionId]: value }));
    };

    const handleSubmit = () => {
        const submission = {
            answers: Object.entries(answers).map(([questionId, answerText]) => ({
                surveyQuestionId: parseInt(questionId),
                answerText,
            }))
        };
        submitSurveyAnswers(submission).then(() => {
            alert("Anket gönderildi!");
        });
    };

    return (
        <div>
            <h2>Anket Soruları</h2>
            {questions.map((q: any) => (
                <div key={q.surveyQuestionId}>
                    <p>{q.questionText}</p>
                    {options.map(opt => (
                        <label key={opt}>
                            <input
                                type="radio"
                                name={`q-${q.surveyQuestionId}`}
                                value={opt}
                                onChange={() => handleChange(q.surveyQuestionId, opt)}
                            />
                            {opt}
                        </label>
                    ))}
                </div>
            ))}
            <button onClick={handleSubmit}>Gönder</button>
        </div>
    );
};

export default SurveyDetail;
