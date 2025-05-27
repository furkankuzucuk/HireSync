import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import '../css/SurveyCreate.css';

const SurveyCreate = () => {
  const [surveyTitle, setSurveyTitle] = useState('');
  const [surveyType, setSurveyType] = useState('');
  const [successMessage, setSuccessMessage] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const navigate = useNavigate();

  const handleCreate = async () => {
    setSuccessMessage('');
    setErrorMessage('');

    const token = localStorage.getItem('token');

    try {
      const response = await fetch('/api/satisfactionsurveys', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          surveyTitle,
          surveyType,
        }),
      });

      if (response.ok) {
        const createdSurvey = await response.json();
        setSuccessMessage('✅ Anket oluşturuldu, şimdi soru ekleyebilirsiniz.');
        setTimeout(() => {
          navigate(`/admin-dashboard/survey-add-question/${createdSurvey.satisfactionSurveyId}`);
        }, 2000);
      } else {
        setErrorMessage('❌ Anket oluşturulamadı.');
      }
    } catch (err) {
      console.error('Anket oluşturma hatası:', err);
      setErrorMessage('❌ Bir hata oluştu.');
    }
  };

  return (
    <div className="survey-create-container">
      <h2 className="text-center mb-4">📋 Yeni Anket Oluştur</h2>

      {successMessage && <div className="alert alert-success">{successMessage}</div>}
      {errorMessage && <div className="alert alert-danger">{errorMessage}</div>}

      <div className="form-group">
        <label>Anket Başlığı</label>
        <input
          type="text"
          className="form-control"
          placeholder="Örn: Çalışan Memnuniyeti"
          value={surveyTitle}
          onChange={(e) => setSurveyTitle(e.target.value)}
          required
        />
      </div>

      <div className="form-group">
        <label>Anket Türü</label>
        <input
          type="text"
          className="form-control"
          placeholder="Örn: Genel, Teknik, Yönetimsel"
          value={surveyType}
          onChange={(e) => setSurveyType(e.target.value)}
          required
        />
      </div>

      <button className="btn btn-primary w-100 mt-3" onClick={handleCreate}>
        ➕ Kaydet ve Soru Ekle
      </button>
    </div>
  );
};

export default SurveyCreate;
