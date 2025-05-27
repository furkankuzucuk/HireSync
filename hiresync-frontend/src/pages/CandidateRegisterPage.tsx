import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Modal } from "react-bootstrap";
import axios from "axios";
import "../css/CandidateRegisterPage.css";

const CandidateRegisterPage = () => {
  const [form, setForm] = useState({
    name: "",
    lastName: "",
    gender: "",
    phone: "",
    address: "",
    birthday: "",
    email: "",
    userName: "",
    password: ""
  });

  const [showModal, setShowModal] = useState(false);
  const navigate = useNavigate();

  const handleChange = (e: any) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleRegister = async () => {
    const emptyFields = Object.entries(form)
      .filter(([_, value]) => value.trim() === "")
      .map(([key]) => key);

    const fieldNames: { [key: string]: string } = {
      name: "Ad",
      lastName: "Soyad",
      gender: "Cinsiyet",
      phone: "Telefon",
      address: "Adres",
      birthday: "Doğum Tarihi",
      email: "E-posta",
      userName: "Kullanıcı Adı",
      password: "Şifre"
    };

    if (emptyFields.length > 0) {
      const missing = emptyFields.map(f => fieldNames[f]).join(", ");
      alert(`Lütfen şu alan(lar)ı doldurun: ${missing}`);
      return;
    }

    try {
      const userResponse = await axios.post("http://localhost:5065/api/users/create-candidate", {
        name: form.name,
        lastName: form.lastName,
        gender: form.gender,
        phone: form.phone,
        address: form.address,
        birthday: form.birthday,
        email: form.email
      });

      const userId = userResponse.data.userId;

      await axios.post("http://localhost:5065/api/login/create", {
        userId: userId,
        userName: form.userName,
        password: form.password,
        mail: form.email,
        createdAt: new Date().toISOString()
      });

      setShowModal(true);
      setTimeout(() => {
        setShowModal(false);
        navigate("/login");
      }, 5000);

    } catch (error: any) {
      if (error.response) {
        alert("Sunucudan gelen hata:\n" + JSON.stringify(error.response.data));
      } else {
        alert("İstek gönderilirken hata oluştu.");
      }
    }
  };

  return (
    <div className="register-container d-flex justify-content-center align-items-center">
      <div className="register-card card shadow">
        <div className="card-header bg-success text-white text-center">
          <h4>Aday Kayıt</h4>
        </div>
        <div className="card-body">
          <input name="name" className="form-control mb-2" placeholder="Ad" onChange={handleChange} />
          <input name="lastName" className="form-control mb-2" placeholder="Soyad" onChange={handleChange} />
          <input name="phone" className="form-control mb-2" placeholder="Telefon" onChange={handleChange} />
          <input name="address" className="form-control mb-2" placeholder="Adres" onChange={handleChange} />
          <input name="birthday" type="date" className="form-control mb-2" onChange={handleChange} />
          <select name="gender" className="form-select mb-3" onChange={handleChange}>
            <option value="">Cinsiyet seçiniz</option>
            <option value="Kadın">Kadın</option>
            <option value="Erkek">Erkek</option>
            <option value="Belirtmek istemiyorum">Belirtmek istemiyorum</option>
          </select>
          <input name="email" type="email" className="form-control mb-2" placeholder="E-posta" onChange={handleChange} />
          <input name="userName" className="form-control mb-2" placeholder="Kullanıcı Adı" onChange={handleChange} />
          <input name="password" type="password" className="form-control mb-3" placeholder="Şifre" onChange={handleChange} />
          <button className="btn btn-primary w-100" onClick={handleRegister}>Kayıt Ol</button>
        </div>
      </div>

      <Modal show={showModal} backdrop="static" keyboard={false}>
        <Modal.Header>
          <Modal.Title>Kayıt Başarılı</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Kaydınız başarıyla tamamlandı. Giriş ekranına yönlendiriliyorsunuz...
        </Modal.Body>
      </Modal>
    </div>
  );
};

export default CandidateRegisterPage;
