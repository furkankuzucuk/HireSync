import React, { useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import axios from 'axios';
import zxcvbn from 'zxcvbn';

const ResetPassword = () => {
  const [params] = useSearchParams();
  const token = params.get("token");

  const [password, setPassword] = useState('');
  const [confirm, setConfirm] = useState('');
  const [done, setDone] = useState(false);
  const [error, setError] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!token) {
      setError("Bağlantı geçersiz.");
      return;
    }

    if (password !== confirm) {
      setError("Şifreler uyuşmuyor.");
      return;
    }

    if (zxcvbn(password).score < 3) {
      setError("Şifre çok zayıf.");
      return;
    }

    try {
      await axios.post('/api/login/reset-password', {
        token,
        newPassword: password,
        confirmPassword: confirm
      });
      setDone(true);
    } catch {
      setError("Token geçersiz veya süresi dolmuş.");
    }
  };

  return (
    <div>
      <h2>🔑 Yeni Şifre Belirle</h2>
      {done ? (
        <p>Şifreniz başarıyla güncellendi. Giriş yapabilirsiniz.</p>
      ) : (
        <form onSubmit={handleSubmit}>
          <input
            type="password"
            placeholder="Yeni şifre"
            value={password}
            onChange={(e) => {
              setPassword(e.target.value);
              setError('');
            }}
          />
          <input
            type="password"
            placeholder="Tekrar girin"
            value={confirm}
            onChange={(e) => setConfirm(e.target.value)}
          />
          {error && <p>{error}</p>}
          <button type="submit">Şifreyi Sıfırla</button>
        </form>
      )}
    </div>
  );
};

export default ResetPassword;
