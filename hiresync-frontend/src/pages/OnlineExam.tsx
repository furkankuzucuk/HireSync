// src/pages/OnlineExam.tsx
import React, { useState } from "react";
import "../css/OnlineExam.css";

const OnlineExam = () => {
  const [examName, setExamName] = useState("");
  const [examDate, setExamDate] = useState("");
  const [createdExamId, setCreatedExamId] = useState<number | null>(null);
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
      body: JSON.stringify({ examName, examDate }),
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
    if (!createdExamId) return alert("Önce sınav oluşturmalısınız.");
    if (!questionText || !correctAnswer) return alert("Soru ve doğru cevap zorunludur.");

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
    <div className="online-exam container mt-4">
      <h2 className="mb-4">📝 Online Sınav Yönetimi</h2>

      {!createdExamId ? (
        <form onSubmit={handleCreateExam} className="form-box shadow-sm">
          <div className="mb-3">
            <label className="form-label">Sınav Başlığı:</label>
            <input
              type="text"
              className="form-control"
              placeholder="Sınav adı"
              value={examName}
              onChange={(e) => setExamName(e.target.value)}
              required
            />
          </div>

          <div className="mb-3">
            <label className="form-label">Sınav Tarihi:</label>
            <input
              type="date"
              className="form-control"
              value={examDate}
              onChange={(e) => setExamDate(e.target.value)}
              required
            />
          </div>

          <button type="submit" className="btn btn-primary w-100">
            ➕ Sınav Oluştur
          </button>
        </form>
      ) : (
        <div className="form-box shadow-sm">
          <h4 className="mb-3">Soru Ekle (Sınav ID: {createdExamId})</h4>

          <div className="mb-3">
            <label className="form-label">Soru Metni:</label>
            <textarea
              className="form-control"
              placeholder="Soru metnini yazın"
              value={questionText}
              onChange={(e) => setQuestionText(e.target.value)}
              required
            />
          </div>

          <div className="mb-3">
            <label className="form-label">Cevap Seçenekleri:</label>
            {answerOptions.map((opt, i) => (
              <input
                key={i}
                type="text"
                className="form-control mb-2"
                placeholder={`Seçenek ${i + 1}`}
                value={opt}
                onChange={(e) => updateOption(i, e.target.value)}
              />
            ))}
          </div>

          <div className="mb-3">
            <label className="form-label">Doğru Cevap:</label>
            <input
              type="text"
              className="form-control"
              placeholder="Doğru cevabı yazın"
              value={correctAnswer}
              onChange={(e) => setCorrectAnswer(e.target.value)}
              required
            />
          </div>

          <button onClick={handleAddQuestion} className="btn btn-success w-100">
            ✅ Soruyu Ekle
          </button>
        </div>
      )}
    </div>
  );
};

export default OnlineExam;
