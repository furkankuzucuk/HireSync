import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const ForgotPassword = () => {
  const [email, setEmail] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!email) {
      setError("Lütfen geçerli bir e-posta adresi girin.");
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
    <div>
      <h2>🔐 Şifre Sıfırlama</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="email"
          placeholder="E-posta adresiniz"
          value={email}
          onChange={(e) => {
            setEmail(e.target.value);
            setError('');
          }}
        />
        {error && <p>{error}</p>}
        <button type="submit" disabled={loading}>
          {loading ? "Gönderiliyor..." : "Sıfırlama Linki Gönder"}
        </button>
      </form>

      {/* ✅ Gönderildikten sonra bilgilendirme */}
      {!loading && error === '' && email !== '' && (
        <p>Mail gönderildi. Giriş ekranına yönlendiriliyorsunuz...</p>
      )}
    </div>
  );
};

export default ForgotPassword;
