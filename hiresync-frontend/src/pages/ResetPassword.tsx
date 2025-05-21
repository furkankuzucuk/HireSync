import React, { useState } from 'react';
import { useSearchParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const ResetPassword: React.FC = () => {
  const [params] = useSearchParams();
  const token = params.get("token");
  const navigate = useNavigate();

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

    // Şifre gücü kontrolü kaldırıldı, tüm şifreler kabul edilecek

    try {
      await axios.post('/api/login/reset-password', {
        token,
        newPassword: password,
        confirmPassword: confirm
      });
      setDone(true);
      toast.success("Şifre başarıyla sıfırlandı. Giriş yapabilirsiniz.");
      setTimeout(() => {
        navigate('/login');
      }, 3000);
    } catch {
      setError("Token geçersiz veya süresi dolmuş.");
      toast.error("Token geçersiz veya süresi dolmuş.");
    }
  };

  return (
    <div style={{ maxWidth: 400, margin: 'auto', padding: 20 }}>
      <ToastContainer position="top-center" />
      <h2>🔑 Yeni Şifre Belirle</h2>
      {done ? (
        <p>Şifreniz başarıyla güncellendi. <a href="/login">Giriş yapabilirsiniz.</a></p>
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
            required
            style={{ width: '100%', padding: 8, marginBottom: 10 }}
            autoComplete="new-password"
          />
          <input
            type="password"
            placeholder="Tekrar girin"
            value={confirm}
            onChange={(e) => setConfirm(e.target.value)}
            required
            style={{ width: '100%', padding: 8, marginBottom: 10 }}
            autoComplete="new-password"
          />
          {error && <p style={{ color: 'red' }}>{error}</p>}
          <button type="submit" style={{ width: '100%', padding: 10 }}>
            Şifreyi Sıfırla
          </button>
        </form>
      )}
    </div>
  );
};

export default ResetPassword;
