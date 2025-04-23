import React, { useState } from 'react';
import '../css/ForgotPassword.css';


const ForgotPassword = () => {
  const [email, setEmail] = useState('');
  const [sent, setSent] = useState(false);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // Backend'e gönderilecek: POST /api/request-reset-password
    console.log("E-posta gönderildi:", email);
    setSent(true);
  };

  return (
    <div style={{ maxWidth: '400px', margin: '80px auto' }}>
      <h2>🔐 Şifre Sıfırlama</h2>
      {sent ? (
        <p>Sıfırlama bağlantısı e-postanıza gönderildi (mock).</p>
      ) : (
        <form onSubmit={handleSubmit}>
          <label>E-Posta:</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
            style={{ width: "100%", padding: "10px", marginBottom: "12px" }}
          />
          <button type="submit">Bağlantı Gönder</button>
        </form>
      )}
    </div>
  );
};

export default ForgotPassword;
