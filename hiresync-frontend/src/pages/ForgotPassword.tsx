import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'animate.css';
import 'react-toastify/dist/ReactToastify.css';
import "../css/ForgotPassword.css";

const ForgotPassword = () => {
  const [email, setEmail] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!email) {
      toast.error("Lütfen geçerli bir e-posta adresi girin.");
      return;
    }

    setLoading(true);
    setError('');

    try {
      await axios.post('/api/login/forgot-password', { email });
      
      // ✅ 2 saniye sonra login ekranına yönlendir
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
    <div className="forgot-password-container">
      <div className="forgot-password-card">
        <h2>🔐 Şifre Sıfırlama</h2>
        <form onSubmit={handleSubmit}>
          <div className="input-group">
            <label htmlFor="email">E-posta adresiniz</label>
            <input
              id="email"
              className="input-field"
              type="email"
              placeholder="E-posta adresiniz"
              value={email}
              onChange={(e) => {
                setEmail(e.target.value);
                setError('');
              }}
            />
          </div>
          {error && <p className="error-message">{error}</p>}
          <button className="submit-button" type="submit" disabled={loading}>
            {loading ? "Gönderiliyor..." : "Sıfırlama Linki Gönder"}
          </button>
        </form>

        {!loading && error === '' && email !== '' && (
          <p className="success-message">Mail gönderildi. Giriş ekranına yönlendiriliyorsunuz...</p>
        )}
      </div>
    </div>
  );
};

export default ForgotPassword;
