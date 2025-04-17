// src/pages/LoginPage.tsx
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { login } from '../services/LoginService';

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    try {
      const response = await login(email, password);
      if (response.role === 'admin') {
        // Token gibi bilgileri burada saklayabilirsin
        navigate('/admin-dashboard');
      } else {
        setError('Yetkisiz giriş');
      }
    } catch (err: any) {
      setError(err.response?.data?.message || 'Sunucu hatası');
    }
  };

  return (
    <div style={{ textAlign: 'center', marginTop: '10%' }}>
      <form onSubmit={handleSubmit}>
        <h2>Giriş Yap</h2>
        <input
          type="text"
          placeholder="E-posta"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        /><br /><br />
        <input
          type="password"
          placeholder="Şifre"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        /><br /><br />
        <button type="submit">Giriş Yap</button>
        {error && <p style={{ color: 'red' }}>{error}</p>}
      </form>
    </div>
  );
};

export default LoginPage;
