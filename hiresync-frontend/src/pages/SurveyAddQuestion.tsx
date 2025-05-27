import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import '../css/SurveyAddQuestion.css';

const SurveyAddQuestion = () => {
  const { id } = useParams();
  const [questionText, setQuestionText] = useState('');
  const [loading, setLoading] = useState(false);

  const handleAdd = async () => {
    const token = localStorage.getItem('token');
    if (!questionText.trim()) {
      alert("Lütfen bir soru girin.");
      return;
    }

    setLoading(true);
    try {
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
        alert('✅ Soru başarıyla eklendi.');
        setQuestionText('');
      } else {
        alert('❌ Soru eklenemedi.');
      }
    } catch (error) {
      console.error("Hata:", error);
      alert("Bir hata oluştu.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="survey-add-container">
      <div className="survey-card shadow">
        <h2 className="text-center mb-4">➕ Yeni Soru Ekle</h2>
        <p className="survey-id">Anket ID: {id}</p>
        <textarea
          className="form-control mb-3"
          rows={4}
          placeholder="Soru metnini buraya girin..."
          value={questionText}
          onChange={(e) => setQuestionText(e.target.value)}
        />
        <button
          className="btn btn-primary w-100"
          onClick={handleAdd}
          disabled={loading}
        >
          {loading ? "Ekleniyor..." : "Soruyu Ekle"}
        </button>
      </div>
    </div>
  );
};

export default SurveyAddQuestion;
