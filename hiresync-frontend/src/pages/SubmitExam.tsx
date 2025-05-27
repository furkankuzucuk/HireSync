import React, { useEffect, useState } from 'react';
import axios from '../services/axiosInstance';
import { useNavigate } from 'react-router-dom';
import { ExamDto, QuestionDto } from '../types/ExamTypes';
import '../css/SubmitExam.css';

const SubmitExam = ({ examId }: { examId: number }) => {
  const navigate = useNavigate();

  const [exam, setExam] = useState<ExamDto | null>(null);
  const [questions, setQuestions] = useState<QuestionDto[]>([]);
  const [answers, setAnswers] = useState<{ [questionId: number]: string }>({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchExam = async () => {
      try {
        const examRes = await axios.get(`/api/exams/${examId}`);
        const questionsRes = await axios.get(`/api/exams/${examId}/questions`);
        setExam(examRes.data);
        setQuestions(questionsRes.data);
      } catch (err) {
        console.error('Veri çekilirken hata oluştu:', err);
      } finally {
        setLoading(false);
      }
    };
    fetchExam();
  }, [examId]);

  const handleAnswerChange = (questionId: number, answer: string) => {
    setAnswers(prev => ({
      ...prev,
      [questionId]: answer
    }));
  };

  const handleSubmit = async () => {
    try {
      await axios.post('/api/userexams/submit', {
        examId,
        answers
      });
      alert('✅ Sınav başarıyla gönderildi!');
      navigate('/worker-dashboard/training');
    } catch (error) {
      console.error('Sınav gönderilemedi:', error);
      alert('❌ Bir hata oluştu.');
    }
  };

  if (loading) return <div className="submit-exam-container">Yükleniyor...</div>;
  if (!exam || !questions.length) return <div className="submit-exam-container">Veriler bulunamadı.</div>;

  return (
    <div className="submit-exam-container">
      <div className="exam-card shadow">
        <h2 className="exam-title">{exam.examName}</h2>
        {questions.map((q: QuestionDto, idx) => (
          <div key={q.questionId} className="question-block">
            <p className="question-text">
              {idx + 1}) {q.questionText}
            </p>
            {q.answerOptions.map((opt, optIdx) => (
              <label key={optIdx} className="option-label">
                <input
                  type="radio"
                  name={`q-${q.questionId}`}
                  value={opt}
                  checked={answers[q.questionId] === opt}
                  onChange={() => handleAnswerChange(q.questionId, opt)}
                />
                {opt}
              </label>
            ))}
          </div>
        ))}
        <button className="btn-submit" onClick={handleSubmit}>
          📤 Sınavı Gönder
        </button>
      </div>
    </div>
  );
};

export default SubmitExam;
