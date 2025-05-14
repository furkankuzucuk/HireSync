import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { SurveyQuestion } from '../types/surveyTypes'; 
// export interface SurveyQuestion {
//   surveyQuestionId: number;
//   questionText: string;
//   questionType: string;
// }

const SurveyEdit = () => {
  const { id } = useParams();
  const [questions, setQuestions] = useState<SurveyQuestion[]>([]);
  const [newQuestion, setNewQuestion] = useState('');

  useEffect(() => {
    fetch(`/api/surveyquestions/survey/${id}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      },
    })
      .then(res => res.json())
      .then(setQuestions);
  }, [id]);

  const handleAdd = async () => {
    const response = await fetch('/api/surveyquestions', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      },
      body: JSON.stringify({
        satisfactionSurveyId: parseInt(id!), // ✅ doğru alan adı
        questionText: newQuestion,
        questionType: 'Memnuniyet Anketi',
      }),
    });

    if (response.ok) {
      setNewQuestion('');
      const updated = await fetch(`/api/surveyquestions/survey/${id}`, {
        headers: { Authorization: `Bearer ${localStorage.getItem('token')}` },
      });
      const refreshed = await updated.json();
      setQuestions(refreshed);
    } else {
      alert('Soru eklenemedi');
    }
  };

  const handleDelete = async (questionId: number) => {
    const confirmed = window.confirm("Bu soruyu silmek istediğinize emin misiniz?");
    if (!confirmed) return;

    const response = await fetch(`/api/surveyquestions/${questionId}`, {
      method: 'DELETE',
      headers: {
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      },
    });

    if (response.ok) {
      setQuestions(prev => prev.filter(q => q.surveyQuestionId !== questionId));
    } else {
      alert('Silinemedi');
    }
  };

  return (
    <div>
      <h2>Anket Sorularını Güncelle</h2>

      <ul>
        {questions.map((q: SurveyQuestion) => (
          <li key={q.surveyQuestionId}>
            {q.questionText}
            <button
              onClick={() => handleDelete(q.surveyQuestionId)}
              style={{ marginLeft: '10px', color: 'red' }}
            >
              Sil
            </button>
          </li>
        ))}
      </ul>

      <h4>Yeni Soru Ekle</h4>
      <textarea
        value={newQuestion}
        onChange={(e) => setNewQuestion(e.target.value)}
        placeholder="Soru metnini yazın"
      />
      <br />
      <button onClick={handleAdd}>Ekle</button>
    </div>
  );
};

export default SurveyEdit;
