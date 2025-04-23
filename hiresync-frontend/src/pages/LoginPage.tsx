import React, { useState } from "react";
import '../css/LoginPage.css';  // CSS dosyasını import et

const LoginPage = ({ onLoginSuccess }: { onLoginSuccess: (role: string) => void }) => {
  const [username, setUsername] = useState<string>(""); // Kullanıcı adı state
  const [password, setPassword] = useState<string>(""); // Şifre state
  const [errorMessage, setErrorMessage] = useState<string>(""); // Hata mesajı state

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (username === "" || password === "") {
      setErrorMessage("Lütfen tüm alanları doldurun.");
    } else {
      setErrorMessage("");

      // Mock veri kullanarak giriş yapalım
      const mockAdminUser = {
        username: "admin",
        password: "admin123",
        role: "Admin"
      };

      const mockWorkerUser = {
        username: "worker",
        password: "worker123",
        role: "Worker"
      };

      // Admin kullanıcıyı kontrol edelim
      if (username === mockAdminUser.username && password === mockAdminUser.password) {
        onLoginSuccess(mockAdminUser.role); // Admin paneline yönlendir
      }
      // Worker kullanıcıyı kontrol edelim
      else if (username === mockWorkerUser.username && password === mockWorkerUser.password) {
        onLoginSuccess(mockWorkerUser.role); // Worker paneline yönlendir
      } else {
        setErrorMessage("Kullanıcı adı veya şifre yanlış.");
      }
    }
  };

  return (
    <div className="login-container">
      <h2>Giriş Yap</h2>
      {errorMessage && <p className="error">{errorMessage}</p>}
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="username">Kullanıcı Adı:</label>
          <input
            id="username"
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            placeholder="Kullanıcı adınızı girin"
            required
          />
        </div>
        <div>
          <label htmlFor="password">Şifre:</label>
          <input
            id="password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Şifrenizi girin"
            required
          />
        </div>
        <button type="submit">Giriş Yap</button>
      </form>
      <div className="footer-links">
        <a href="/forgot-password">Şifrenizi Unuttunuz?</a> | 
        <a href="/register">Kayıt Ol</a>
      </div>
    </div>
  );
};

export default LoginPage;
