import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { SurveyQuestion } from '../types/surveyTypes';
import '../css/SurveyEdit.css';

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
    if (!newQuestion.trim()) return alert("Soru metni boş olamaz");

    const response = await fetch('/api/surveyquestions', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      },
      body: JSON.stringify({
        satisfactionSurveyId: parseInt(id!),
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
      alert('❌ Soru eklenemedi.');
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
      alert('❌ Soru silinemedi.');
    }
  };

  return (
    <div className="survey-edit-container">
      <h2 className="text-center mb-4">🛠️ Anket Sorularını Güncelle</h2>

      <ul className="question-list">
        {questions.map((q: SurveyQuestion) => (
          <li key={q.surveyQuestionId} className="question-item">
            <span>{q.questionText}</span>
            <button
              className="btn btn-sm btn-outline-danger"
              onClick={() => handleDelete(q.surveyQuestionId)}
            >
              ❌ Sil
            </button>
          </li>
        ))}
      </ul>

      <div className="add-question-form mt-4">
        <h5>➕ Yeni Soru Ekle</h5>
        <textarea
          className="form-control"
          rows={3}
          value={newQuestion}
          onChange={(e) => setNewQuestion(e.target.value)}
          placeholder="Yeni soru metnini giriniz..."
        />
        <button className="btn btn-success mt-2" onClick={handleAdd}>
          Soruyu Ekle
        </button>
      </div>
    </div>
  );
};

export default SurveyEdit;
