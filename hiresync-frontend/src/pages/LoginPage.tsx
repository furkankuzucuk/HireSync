import React, { useState } from "react";
import axios from "axios";
import "../css/LoginPage.css";

interface LoginPageProps {
  onLoginSuccess: (role: string) => void;
}

const LoginPage: React.FC<LoginPageProps> = ({ onLoginSuccess }) => {
  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [errorMessage, setErrorMessage] = useState<string>("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (username === "" || password === "") {
      setErrorMessage("Lütfen tüm alanları doldurun.");
      return;
    }

    try {
      const response = await axios.post("http://localhost:5065/api/login/authenticate", {
        userName: username,
        password: password
      });

      const { token, role } = response.data;

      // Başarıyla giriş yapıldı, bilgileri localStorage'a kaydediyoruz
      localStorage.setItem("token", token);
      localStorage.setItem("role", role);
      localStorage.setItem("username", username); // Kullanıcı adını da kaydediyoruz
      localStorage.setItem("tokenExpiration", (Date.now() + 3600 * 1000).toString()); // 1 saatlik geçerlilik

      onLoginSuccess(role); // App.tsx'e rolü gönderiyoruz, yönlendirme orada oluyor
    } catch (error) {
      console.error(error);
      setErrorMessage("Kullanıcı adı veya şifre yanlış."); // Hatalı girişte mesaj göster
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