import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import '../css/SurveyAddQuestion.css';

const SurveyAddQuestion = () => {
  const { id } = useParams();
  const [questionText, setQuestionText] = useState('');
  const [loading, setLoading] = useState(false);
  const [successMessage, setSuccessMessage] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  const handleAdd = async () => {
    const token = localStorage.getItem('token');
    setSuccessMessage('');
    setErrorMessage('');

    if (!questionText.trim()) {
      setErrorMessage("⚠️ Lütfen bir soru girin.");
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
        setSuccessMessage('✅ Soru başarıyla eklendi.');
        setQuestionText('');
      } else {
        setErrorMessage('❌ Soru eklenemedi.');
      }
    } catch (error) {
      console.error("Hata:", error);
      setErrorMessage("❌ Bir hata oluştu.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="survey-add-container">
      <div className="survey-card shadow">
        <h2 className="text-center mb-4">➕ Yeni Soru Ekle</h2>
        <p className="survey-id">Anket ID: {id}</p>

        {successMessage && <div className="alert alert-success">{successMessage}</div>}
        {errorMessage && <div className="alert alert-danger">{errorMessage}</div>}

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
