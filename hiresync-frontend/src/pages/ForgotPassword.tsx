import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import "../css/ForgotPassword.css";

const ForgotPassword = () => {
  const [email, setEmail] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!email) {
      toast.error("Lütfen geçerli bir e-posta adresi girin.");
      return;
    }

    setLoading(true);
    setError('');
    setSuccess(false);

    try {
      await axios.post('/api/login/forgot-password', { email });
      setSuccess(true);

      setTimeout(() => {
        navigate('/login');
      }, 2000);
    } catch {
      setError("Bir hata oluştu. Lütfen tekrar deneyin.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="forgot-password-container d-flex justify-content-center align-items-center">
      <div className="forgot-password-card shadow">
        <h2 className="text-center mb-4">🔐 Şifre Sıfırlama</h2>
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label htmlFor="email" className="form-label">E-posta adresiniz</label>
            <input
              id="email"
              className="form-control"
              type="email"
              placeholder="E-posta adresiniz"
              value={email}
              onChange={(e) => {
                setEmail(e.target.value);
                setError('');
              }}
            />
          </div>
          {error && <p className="text-danger">{error}</p>}
          <button className="btn btn-primary w-100" type="submit" disabled={loading}>
            {loading ? "Gönderiliyor..." : "Sıfırlama Linki Gönder"}
          </button>
        </form>

        {success && !loading && !error && (
          <div className="alert alert-success mt-3" role="alert">
            Mail gönderildi. Giriş ekranına yönlendiriliyorsunuz...
          </div>
        )}
      </div>
      <ToastContainer />
    </div>
  );
};

export default ForgotPassword;
