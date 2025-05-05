// src/pages/CandidateRegisterPage.tsx
import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import axios from "axios";

const CandidateRegisterPage = () => {
  const [form, setForm] = useState({
    name: "",
    lastName: "",
    email: "",
    gender: "",
    phone: "",
    address: "",
    birthday: "",
  });

  const navigate = useNavigate();
  const location = useLocation();
  const jobListId = new URLSearchParams(location.search).get("jobListId");

  const handleChange = (e: any) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async () => {
    try {
      // 1. Aday olarak kayıt ol
      const userRes = await axios.post("http://localhost:5065/api/users/create-candidate", form);
      const userId = userRes.data.userId;

      // 2. Başvuru oluştur
      if (jobListId) {
        await axios.post("http://localhost:5065/api/jobapplications", {
          jobListId: parseInt(jobListId),
          userId: userId,
          appDate: new Date().toISOString(),
          status: "Pending"
        });
      }

      alert("Kayıt ve başvuru başarılı!");
      navigate("/login");

    } catch (err) {
      console.error(err);
      alert("İşlem sırasında hata oluştu.");
    }
  };

  return (
    <div className="container mt-5">
      <h2>Aday Kayıt Formu</h2>
      <input name="name" className="form-control mb-2" placeholder="Ad" onChange={handleChange} />
      <input name="lastName" className="form-control mb-2" placeholder="Soyad" onChange={handleChange} />
      <input name="email" type="email" className="form-control mb-2" placeholder="Mail" onChange={handleChange} />
      <input name="phone" className="form-control mb-2" placeholder="Telefon" onChange={handleChange} />
      <input name="gender" className="form-control mb-2" placeholder="Cinsiyet" onChange={handleChange} />
      <input name="birthday" type="date" className="form-control mb-2" onChange={handleChange} />
      <input name="address" className="form-control mb-2" placeholder="Adres" onChange={handleChange} />

      <button className="btn btn-primary mt-3" onClick={handleSubmit}>
        Kayıt Ol ve Başvur
      </button>
    </div>
  );
};

export default CandidateRegisterPage;
