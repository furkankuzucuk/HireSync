import React, { useEffect, useState } from 'react';
import axios from '../services/axiosInstance';
import { useNavigate } from 'react-router-dom';
import { ExamDto, QuestionDto } from '../types/ExamTypes';

const SubmitExam = ({ examId }: { examId: number }) => {
    const navigate = useNavigate();

    const [exam, setExam] = useState<ExamDto | null>(null);
    const [questions, setQuestions] = useState<QuestionDto[]>([]);
    const [answers, setAnswers] = useState<{ [questionId: number]: string }>({});

    useEffect(() => {
        const fetchExam = async () => {
            try {
                const examRes = await axios.get(`/api/exams/${examId}`);
                const questionsRes = await axios.get(`/api/exams/${examId}/questions`);
                setExam(examRes.data);
                setQuestions(questionsRes.data);
            } catch (err) {
                console.error('Veri çekilirken hata oluştu:', err);
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
                examId: examId,
                answers: answers
            });
            alert('Sınav başarıyla gönderildi!');
            navigate('/worker-dashboard/training');
        } catch (error) {
            console.error('Sınav gönderilemedi:', error);
            alert('Bir hata oluştu.');
        }
    };

    if (!exam) return <div>Loading exam...</div>;
    if (!questions.length) return <div>Loading questions...</div>;

    return (
        <div>
            <h2>{exam.examName}</h2>
            {questions.map((q: QuestionDto) => (
                <div key={q.questionId}>
                    <p>{q.questionText}</p>
                    {q.answerOptions.map((opt, idx) => (
                        <label key={idx} style={{ display: 'block', marginLeft: '1rem' }}>
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
            <button onClick={handleSubmit} style={{ marginTop: '20px' }}>
                Sınavı Gönder
            </button>
        </div>
    );
};

export default SubmitExam;
