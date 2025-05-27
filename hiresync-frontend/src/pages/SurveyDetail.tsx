import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { submitSurveyAnswers } from '../services/surveyAnswerService';
import '../css/SurveyDetail.css';

const options = [
  "Kesinlikle Katılıyorum",
  "Katılıyorum",
  "Kararsızım",
  "Katılmıyorum",
  "Kesinlikle Katılmıyorum"
];

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
      answers: Object.entries(answers).map(([questionId, answer]) => ({
        surveyQuestionId: parseInt(questionId),
        answer,
      }))
    };

    try {
      await submitSurveyAnswers(submission);
      alert("✅ Anket başarıyla gönderildi!");
      navigate('/worker-dashboard/surveys');
    } catch (error) {
      console.error("Gönderim hatası:", error);
      alert("❌ Anket gönderilemedi. Lütfen tekrar deneyin.");
    }
  };

  return (
    <div className="survey-detail-container">
      <h2 className="text-center mb-4">📝 Anket Soruları</h2>

      {questions.map((q: any) => (
        <div key={q.surveyQuestionId} className="question-box">
          <p className="question-text">{q.questionText}</p>
          {options.map(opt => (
            <label key={opt} className="option-label">
              <input
                type="radio"
                name={`q-${q.surveyQuestionId}`}
                value={opt}
                onChange={() => handleChange(q.surveyQuestionId, opt)}
                checked={answers[q.surveyQuestionId] === opt}
              />
              {opt}
            </label>
          ))}
        </div>
      ))}

      {questions.length > 0 && (
        <button className="btn btn-success w-100 mt-4" onClick={handleSubmit}>
          Anketi Gönder
        </button>
      )}
    </div>
  );
};

export default SurveyDetail;
