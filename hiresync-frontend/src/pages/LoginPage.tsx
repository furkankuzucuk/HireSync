import React, { useState } from "react";
import '../css/LoginPage.css';  // CSS dosyasını import et

const LoginPage = () => {
  const [email, setEmail] = useState<string>(""); // E-posta state
  const [password, setPassword] = useState<string>(""); // Şifre state
  const [errorMessage, setErrorMessage] = useState<string>(""); // Hata mesajı state

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (email === "" || password === "") {
      setErrorMessage("Lütfen tüm alanları doldurun.");
    } else {
      setErrorMessage("");
      // Burada API çağrısı yapılabilir veya giriş işlemi gerçekleştirilebilir
      alert("Giriş başarılı!");
    }
  };

  function api(){
    
  }
  return (
    <div className="login-container">
      <h2>Giriş Yap</h2>
      {errorMessage && <p className="error">{errorMessage}</p>}
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="email">E-posta:</label>
          <input
            id="email"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="E-posta adresinizi girin"
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
