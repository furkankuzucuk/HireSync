import React, { useState } from 'react';
import { useParams } from 'react-router-dom';

const SurveyAddQuestion = () => {
  const { id } = useParams();
  const [questionText, setQuestionText] = useState('');

  const handleAdd = async () => {
    const token = localStorage.getItem('token');

    const response = await fetch('/api/surveyquestions', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify({
        satisfactionSurveyId: parseInt(id!),
        questionText,
        questionType: "Çoktan Seçmeli" 
      }),
    });

    if (response.ok) {
      alert('✅ Soru eklendi.');
      setQuestionText('');
    } else {
      alert('❌ Soru eklenemedi.');
    }
  };

  return (
    <div>
      <h2>Soru Ekle (Anket ID: {id})</h2>
      <textarea
        placeholder="Soru metni girin"
        value={questionText}
        onChange={(e) => setQuestionText(e.target.value)}
      />
      <br />
      <button onClick={handleAdd}>Soruyu Ekle</button>
    </div>
  );
};

export default SurveyAddQuestion;
