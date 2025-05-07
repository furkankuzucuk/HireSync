import React, { useState } from 'react';
import '../css/ForgotPassword.css';

const ForgotPassword = () => {
  const [email, setEmail] = useState('');
  const [sent, setSent] = useState(false);
  const [error, setError] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!email) {
      setError("Lütfen geçerli bir e-posta adresi girin.");
      return;
    }
    // Backend'e gönderilecek: POST /api/request-reset-password
    console.log("E-posta gönderildi:", email);
    setSent(true);
  };

  return (
    <div className="forgot-password-container">
      <div className="forgot-password-card">
        <h2>🔐 Şifre Sıfırlama</h2>
        {sent ? (
          <p className="success-message">Sıfırlama bağlantısı e-postanıza gönderildi.</p>
        ) : (
          <form onSubmit={handleSubmit}>
            <div className="input-group">
              <label htmlFor="email">E-Posta</label>
              <input
                type="email"
                id="email"
                value={email}
                onChange={(e) => {
                  setEmail(e.target.value);
                  setError(''); // Hata mesajını temizle
                }}
                required
                className="input-field"
                placeholder="E-posta adresinizi girin"
              />
              {error && <p className="error-message">{error}</p>}
            </div>
            <button type="submit" className="submit-button">Bağlantı Gönder</button>
          </form>
        )}
      </div>
    </div>
  );
};

export default ForgotPassword;
