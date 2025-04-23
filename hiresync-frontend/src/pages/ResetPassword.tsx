import React, { useState } from 'react';
import '../css/ResetPassword.css';
import { useSearchParams } from 'react-router-dom';

const ResetPassword = () => {
  const [params] = useSearchParams();
  const token = params.get("token");

  const [password, setPassword] = useState('');
  const [confirm, setConfirm] = useState('');
  const [done, setDone] = useState(false);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (password !== confirm) {
      alert("Şifreler eşleşmiyor");
      return;
    }

    // Backend'e gönderilecek: POST /api/reset-password
    console.log("Yeni şifre:", password, "Token:", token);
    setDone(true);
  };

  return (
    <div style={{ maxWidth: '400px', margin: '80px auto' }}>
      <h2>🔑 Yeni Şifre Belirle</h2>
      {done ? (
        <p>Şifreniz başarıyla sıfırlandı! Artık giriş yapabilirsiniz.</p>
      ) : (
        <form onSubmit={handleSubmit}>
          <label>Yeni Şifre:</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
            style={{ width: "100%", padding: "10px", marginBottom: "12px" }}
          />

          <label>Şifreyi Tekrar Girin:</label>
          <input
            type="password"
            value={confirm}
            onChange={(e) => setConfirm(e.target.value)}
            required
            style={{ width: "100%", padding: "10px", marginBottom: "16px" }}
          />

          <button type="submit">Şifreyi Sıfırla</button>
        </form>
      )}
    </div>
  );
};

export default ResetPassword;
