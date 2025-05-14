import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

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
        departmentId: 1, // İstersen dropdown ile dinamik yaparız
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
    <div>
      <h2>Yeni Anket Oluştur</h2>
      <input
        type="text"
        placeholder="Anket Başlığı"
        value={surveyTitle}
        onChange={(e) => setSurveyTitle(e.target.value)}
      />
      <br />
      <input
        type="text"
        placeholder="Anket Türü"
        value={surveyType}
        onChange={(e) => setSurveyType(e.target.value)}
      />
      <br />
      <button onClick={handleCreate}>Kaydet ve Soru Ekle</button>
    </div>
  );
};

export default SurveyCreate;
