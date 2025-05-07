import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { Modal } from "react-bootstrap";
import axios from "axios";

const CandidateRegisterStep2 = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const { userId, jobListId } = location.state;

  const [loginData, setLoginData] = useState({
    email: "",
    userName: "",
    password: ""
  });

  const [showModal, setShowModal] = useState(false);

  const handleChange = (e: any) => {
    setLoginData({ ...loginData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async () => {
    try {
      await axios.post("http://localhost:5065/api/login/create", {
        userId: userId,
        userName: loginData.userName,
        password: loginData.password,
        mail: loginData.email,
        createdAt: new Date().toISOString()
      });

      setShowModal(true);

      setTimeout(() => {
        setShowModal(false);
        navigate("/login");
      }, 5000);
    } catch (error) {
      console.error("Login kayıt hatası:", error);
      alert("Login bilgileri kaydedilirken hata oluştu.");
    }
  };

  return (
    <div className="container d-flex justify-content-center align-items-center" style={{ minHeight: "90vh" }}>
      <div className="card shadow-lg border-0 w-100" style={{ maxWidth: "500px" }}>
        <div className="card-header bg-secondary text-white text-center py-3">
          <h4 className="mb-0">Giriş Bilgileri</h4>
        </div>
        <div className="card-body">
          <div className="mb-3">
            <label className="form-label">E-posta</label>
            <input type="email" name="email" className="form-control" onChange={handleChange} required />
          </div>
          <div className="mb-3">
            <label className="form-label">Kullanıcı Adı</label>
            <input name="userName" className="form-control" onChange={handleChange} required />
          </div>
          <div className="mb-3">
            <label className="form-label">Şifre</label>
            <input type="password" name="password" className="form-control" onChange={handleChange} required />
          </div>

          <button className="btn btn-success w-100" onClick={handleSubmit}>
            Kaydı Tamamla
          </button>
        </div>
      </div>

      {/* Modal */}
      <Modal show={showModal} backdrop="static" keyboard={false}>
        <Modal.Header>
          <Modal.Title>Kayıt Başarılı</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Giriş bilgileriniz kaydedildi. İlan detaylarına ve başvuruya yönlendiriliyorsunuz...
        </Modal.Body>
      </Modal>
    </div>
  );
};

export default CandidateRegisterStep2;
