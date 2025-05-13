import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { submitSurveyAnswers, getSurveyQuestions } from '../services/SurveyService';
import axios from 'axios'; // Added axios import
import '../css/SurveyForm.css';

interface SurveyAnswer {
  surveyAnswerId: number;
  answer: string;
}

interface SurveyQuestion {
  surveyQuestionId: number;
  questionText: string;
  questionType: string; // 'MultipleChoice' or 'Text'
  surveyAnswers: SurveyAnswer[];
}

const SurveyForm = () => {
  const { surveyId } = useParams<{ surveyId: string }>();
  const navigate = useNavigate();
  const [questions, setQuestions] = useState<SurveyQuestion[]>([]);
  const [surveyTitle, setSurveyTitle] = useState('');
  const [answers, setAnswers] = useState<Record<number, string>>({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchSurveyData = async () => {
      try {
        // Fetch survey details
        const surveyResponse = await axios.get(`http://localhost:5065/api/satisfactionsurveys/${surveyId}`, {
          headers: {
            'Authorization': `Bearer ${localStorage.getItem('authToken')}`
          }
        });
        setSurveyTitle(surveyResponse.data.title);

        // Fetch survey questions
        const questionsResponse = await getSurveyQuestions(Number(surveyId));
        setQuestions(questionsResponse);

        // Initialize empty answers
        const initialAnswers: Record<number, string> = {};
        questionsResponse.forEach((question: SurveyQuestion) => { // Added type annotation
          initialAnswers[question.surveyQuestionId] = '';
        });
        setAnswers(initialAnswers);
      } catch (error) {
        console.error('Error fetching survey data:', error);
        alert('Anket yüklenirken bir hata oluştu.');
      } finally {
        setLoading(false);
      }
    };

    fetchSurveyData();
  }, [surveyId]);

  const handleAnswerChange = (questionId: number, answer: string) => {
    setAnswers((prevAnswers) => ({
      ...prevAnswers,
      [questionId]: answer,
    }));
  };

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    const unansweredQuestions = questions.filter(
      (question: SurveyQuestion) => !answers[question.surveyQuestionId]?.trim() // Added type annotation
    );

    if (unansweredQuestions.length > 0) {
      alert('Lütfen tüm soruları cevaplayın.');
      return;
    }

    const userIdStr = localStorage.getItem('userId');
    if (!userIdStr) {
      alert('Kullanıcı ID bulunamadı. Lütfen tekrar giriş yapın.');
      return;
    }

    const userId = Number(userIdStr);
    if (isNaN(userId)) {
      alert('Geçersiz kullanıcı ID.');
      return;
    }

    const surveyData = {
      satisfactionSurveyId: Number(surveyId),
      answers: Object.entries(answers).map(([questionId, answer]) => ({
        questionId: Number(questionId),
        answer: answer,
      })),
    };

    try {
      await submitSurveyAnswers(userId, surveyData);
      alert('Anket başarıyla gönderildi!');
      navigate('/worker-dashboard/surveys');
    } catch (error) {
      console.error('Anket gönderim hatası:', error);
      alert('Anket gönderilirken bir hata oluştu.');
    }
  };

  if (loading) return <div>Anket yükleniyor...</div>;

  return (
    <div className="survey-form-container">
      <h2>{surveyTitle}</h2>
      <form onSubmit={handleSubmit}>
        {questions.map((question: SurveyQuestion) => ( // Added type annotation
          <div key={question.surveyQuestionId} className="question-card">
            <h4>{question.questionText}</h4>
            <div className="answers-container">
              {question.questionType === 'MultipleChoice' ? (
                question.surveyAnswers.map((answer: SurveyAnswer) => ( // Added type annotation
                  <div key={answer.surveyAnswerId} className="answer-option">
                    <input
                      type="radio"
                      id={`question-${question.surveyQuestionId}-answer-${answer.surveyAnswerId}`}
                      name={`question-${question.surveyQuestionId}`}
                      value={answer.answer}
                      checked={answers[question.surveyQuestionId] === answer.answer}
                      onChange={() => handleAnswerChange(question.surveyQuestionId, answer.answer)}
                      required
                    />
                    <label htmlFor={`question-${question.surveyQuestionId}-answer-${answer.surveyAnswerId}`}>
                      {answer.answer}
                    </label>
                  </div>
                ))
              ) : (
                <textarea
                  value={answers[question.surveyQuestionId] || ''}
                  onChange={(e) => handleAnswerChange(question.surveyQuestionId, e.target.value)}
                  placeholder="Cevabınızı yazın..."
                  required
                />
              )}
            </div>
          </div>
        ))}
        <div className="form-actions">
          <button type="button" onClick={() => navigate('/worker-dashboard/surveys')} className="cancel-btn">
            İptal
          </button>
          <button type="submit" className="submit-btn">
            Anketi Gönder
          </button>
        </div>
      </form>
    </div>
  );
};

export default SurveyForm;