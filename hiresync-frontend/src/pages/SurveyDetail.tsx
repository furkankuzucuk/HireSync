import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { submitSurveyAnswers } from '../services/surveyAnswerService';

const options = ["Kesinlikle Katılıyorum", "Katılıyorum", "Kararsızım", "Katılmıyorum", "Kesinlikle Katılmıyorum"];

const SurveyDetail = () => {
    const { id } = useParams();
    const navigate = useNavigate();
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

    const handleSubmit = async () => {
        const submission = {
            satisfactionSurveyId: parseInt(id!),
            answers: Object.entries(answers).map(([questionId, answerText]) => ({
                surveyQuestionId: parseInt(questionId),
                answerText,
            }))
        };

        try {
            console.log("Gönderilen veri:", submission);
            await submitSurveyAnswers(submission);
            alert("✅ Anket başarıyla gönderildi!");
            navigate('/worker-dashboard/surveys'); // Anket listesine dön
        } catch (error) {
            console.error("Gönderim hatası:", error);
            alert("❌ Anket gönderilemedi. Lütfen tekrar deneyin.");
        }
    };

    return (
        <div>
            <h2>Anket Soruları</h2>
            {questions.map((q: any) => (
                <div key={q.surveyQuestionId}>
                    <p>{q.questionText}</p>
                    {options.map(opt => (
                        <label key={opt} style={{ display: 'block', marginLeft: '20px' }}>
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
