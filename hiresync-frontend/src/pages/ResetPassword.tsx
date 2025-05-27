import React, { useState } from 'react';
import { useSearchParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import '../css/ResetPassword.css';

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

    try {
      await axios.post('/api/login/reset-password', {
        token,
        newPassword: password,
        confirmPassword: confirm
      });
      setDone(true);
      toast.success("Şifre başarıyla sıfırlandı.");
      setTimeout(() => {
        navigate('/login');
      }, 3000);
    } catch {
      setError("Token geçersiz veya süresi dolmuş.");
      toast.error("Token geçersiz veya süresi dolmuş.");
    }
  };

  return (
    <div className="reset-container">
      <ToastContainer position="top-center" />
      <div className="reset-card">
        <h2 className="text-center">🔑 Yeni Şifre Belirle</h2>
        {done ? (
          <p className="text-success text-center">
            Şifreniz başarıyla güncellendi. <a href="/login">Giriş yap</a>
          </p>
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
              autoComplete="new-password"
            />
            <input
              type="password"
              placeholder="Yeni şifre (tekrar)"
              value={confirm}
              onChange={(e) => setConfirm(e.target.value)}
              required
              autoComplete="new-password"
            />
            {error && <div className="error-text">{error}</div>}
            <button type="submit">Şifreyi Sıfırla</button>
          </form>
        )}
      </div>
    </div>
  );
};

export default ResetPassword;
