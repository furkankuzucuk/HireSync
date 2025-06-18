import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import '../css/ForgotPassword.css';

const ForgotPassword = () => {
  const [email, setEmail] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!email) {
      setError("Lütfen geçerli bir e-posta adresi girin.");
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
      }, 2500);
    } catch {
      setError("Bir hata oluştu. Lütfen tekrar deneyin.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="forgot-password-container">
      <div className="forgot-password-card">
        <h2 className="text-gradient">🔐 Şifremi Unuttum</h2>
        <form onSubmit={handleSubmit}>
          <label htmlFor="email" className="form-label">E-posta adresiniz</label>
          <input
            id="email"
            className="form-control"
            type="email"
            placeholder="ornek@eposta.com"
            value={email}
            onChange={(e) => {
              setEmail(e.target.value);
              setError('');
            }}
          />
          {error && <div className="alert alert-danger mt-3">{error}</div>}

          <button className="btn-reset mt-3" type="submit" disabled={loading}>
            {loading ? "Gönderiliyor..." : "Sıfırlama Linki Gönder"}
          </button>
        </form>

        {success && !loading && !error && (
          <div className="alert alert-success mt-3">
            <span>✔️</span>
            <span>Mail gönderildi. Giriş ekranına yönlendiriliyorsunuz...</span>
          </div>
        )}

        <a href="/login" className="back-link">← Giriş ekranına dön</a>
      </div>
    </div>
  );
};

export default ForgotPassword;
