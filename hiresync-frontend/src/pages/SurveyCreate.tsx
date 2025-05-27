import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import '../css/SurveyCreate.css';

const SurveyCreate = () => {
  const [surveyTitle, setSurveyTitle] = useState('');
  const [surveyType, setSurveyType] = useState('');
  const navigate = useNavigate();

  const handleCreate = async () => {
    const token = localStorage.getItem('token');

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
      alert('✅ Anket oluşturuldu, şimdi soru ekleyebilirsiniz.');
      navigate(`/admin-dashboard/survey-add-question/${createdSurvey.satisfactionSurveyId}`);
    } else {
      alert('❌ Anket oluşturulamadı.');
    }
  };

  return (
    <div className="survey-create-container">
      <h2 className="text-center mb-4">📋 Yeni Anket Oluştur</h2>

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
