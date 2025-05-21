import React, { useState, useEffect } from "react";

const OnlineExam = () => {
  const [examName, setExamName] = useState("");
  const [examDate, setExamDate] = useState("");
  const [createdExamId, setCreatedExamId] = useState<number | null>(null);

  // Soru alanları
  const [questionText, setQuestionText] = useState("");
  const [answerOptions, setAnswerOptions] = useState(["", "", "", ""]);
  const [correctAnswer, setCorrectAnswer] = useState("");

  const handleCreateExam = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!examName || !examDate) {
      alert("Sınav adı ve tarihi gerekli.");
      return;
    }

    const response = await fetch("/api/exams", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
      body: JSON.stringify({
        examName,
        examDate,
      }),
    });

    if (response.ok) {
      const data = await response.json();
      setCreatedExamId(data.examId);
      alert("Sınav başarıyla oluşturuldu. Şimdi sorular ekleyebilirsiniz.");
    } else {
      alert("Sınav oluşturulamadı.");
    }
  };

  const handleAddQuestion = async () => {
    if (!createdExamId) {
      alert("Önce sınav oluşturmalısınız.");
      return;
    }
    if (!questionText || !correctAnswer) {
      alert("Soru metni ve doğru cevap zorunlu.");
      return;
    }

    const response = await fetch("/api/questions", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
      body: JSON.stringify({
        examId: createdExamId,
        questionText,
        answerOptions,
        correctAnswer,
      }),
    });

    if (response.ok) {
      alert("Soru başarıyla eklendi.");
      setQuestionText("");
      setAnswerOptions(["", "", "", ""]);
      setCorrectAnswer("");
    } else {
      alert("Soru eklenemedi.");
    }
  };

  const updateOption = (index: number, value: string) => {
    const newOptions = [...answerOptions];
    newOptions[index] = value;
    setAnswerOptions(newOptions);
  };

  return (
    <div className="tab-content">
      <h2>📝 Online Sınav Yönetimi</h2>

      {!createdExamId ? (
        <form onSubmit={handleCreateExam}>
          <label>Sınav Başlığı:</label>
          <input
            type="text"
            placeholder="Sınav adı"
            value={examName}
            onChange={(e) => setExamName(e.target.value)}
          />
          <label>Sınav Tarihi:</label>
          <input
            type="date"
            value={examDate}
            onChange={(e) => setExamDate(e.target.value)}
          />
          <button className="btn green" type="submit">
            Sınav Oluştur
          </button>
        </form>
      ) : (
        <>
          <h3>Soru Ekle (Sınav ID: {createdExamId})</h3>
          <label>Soru Metni:</label>
          <textarea
            placeholder="Soru metnini yazın"
            value={questionText}
            onChange={(e) => setQuestionText(e.target.value)}
          />
          <label>Cevap Seçenekleri:</label>
          {answerOptions.map((option, i) => (
            <input
              key={i}
              type="text"
              placeholder={`Seçenek ${i + 1}`}
              value={option}
              onChange={(e) => updateOption(i, e.target.value)}
            />
          ))}
          <label>Doğru Cevap:</label>
          <input
            type="text"
            placeholder="Doğru cevabı yazın"
            value={correctAnswer}
            onChange={(e) => setCorrectAnswer(e.target.value)}
          />
          <button onClick={handleAddQuestion} className="btn green">
            Soruyu Ekle
          </button>
        </>
      )}
    </div>
  );
};

export default OnlineExam;
