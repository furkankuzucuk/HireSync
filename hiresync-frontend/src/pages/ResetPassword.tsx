import React, { useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import zxcvbn from 'zxcvbn'; // Şifre gücünü ölçmek için kullanılır
import '../css/ResetPassword.css'; // CSS dosyasını unutma

const ResetPassword = () => {
  const [params] = useSearchParams();
  const token = params.get("token");

  const [password, setPassword] = useState('');
  const [confirm, setConfirm] = useState('');
  const [done, setDone] = useState(false);
  const [strength, setStrength] = useState(0); // Şifre gücü
  const [error, setError] = useState('');

  const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newPassword = e.target.value;
    setPassword(newPassword);
    const result = zxcvbn(newPassword); // Şifreyi değerlendir
    setStrength(result.score); // Skoru güncelle
    setError('');
  };

  const handleConfirmChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setConfirm(e.target.value);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (password !== confirm) {
      setError('Şifreler eşleşmiyor.');
      return;
    }

    if (strength < 3) {
      setError('Şifreniz yeterince güçlü değil. Lütfen daha karmaşık bir şifre girin.');
      return;
    }

    try {
      // Burada API'ye reset şifresi gönderilecek
      console.log('Yeni şifre:', password, 'Token:', token);
      // Şifre sıfırlama işlemi başarılıysa
      setDone(true);
    } catch (err) {
      setError('Bir hata oluştu. Lütfen tekrar deneyin.');
    }
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
            onChange={handlePasswordChange}
            required
            style={{ width: "100%", padding: "10px", marginBottom: "12px" }}
          />

          {/* Şifre Güçlü Zayıf Barı */}
          <div className="password-strength-bar">
            <div
              className="password-strength"
              style={{ width: `${(strength + 1) * 20}%` }}
            ></div>
          </div>

          <label>Şifreyi Tekrar Girin:</label>
          <input
            type="password"
            value={confirm}
            onChange={handleConfirmChange}
            required
            style={{ width: "100%", padding: "10px", marginBottom: "16px" }}
          />

          {/* Hata Mesajları */}
          {error && <p className="error-message">{error}</p>}

          <button type="submit">Şifreyi Sıfırla</button>
        </form>
      )}
    </div>
  );
};

export default ResetPassword;
