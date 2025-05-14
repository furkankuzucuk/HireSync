import React, { useState } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";
import "animate.css";
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer, toast } from "react-toastify";
import hiresyncLogo from "../image/hiresync.jpeg";
import "../css/LoginPage.css";

interface LoginPageProps {
  onLoginSuccess: (role: string) => void;
}

const LoginPage: React.FC<LoginPageProps> = ({ onLoginSuccess }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [shake, setShake] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!username || !password) {
      setShake(true);
      toast.error("Lütfen tüm alanları doldurun.");
      return;
    }

    setIsLoading(true);
    try {
      const response = await axios.post("http://localhost:5065/api/login/authenticate", {
        userName: username,
        password: password,
      });

      const { token, role } = response.data;

      localStorage.setItem("token", token);
      localStorage.setItem("role", role);
      localStorage.setItem("username", username);
      localStorage.setItem("tokenExpiration", (Date.now() + 3600 * 1000).toString());

      toast.success("Giriş başarılı! Yönlendiriliyorsunuz...");
      setTimeout(() => {
        onLoginSuccess(role);
      }, 2000);
    } catch (error) {
      setShake(true);
      toast.error("Kullanıcı adı veya şifre hatalı!");
    } finally {
      setIsLoading(false);
      setTimeout(() => setShake(false), 800);
    }
  };

  return (
    <div className="login-background d-flex justify-content-center align-items-center vh-100">
      <div
        className={`card shadow p-4 animate__animated ${
          shake ? "animate__shakeX" : "animate__fadeIn"
        }`}
        style={{ width: "100%", maxWidth: "400px", borderRadius: "12px" }}
      >
        <ToastContainer position="top-center" />
        <div className="text-center mb-3">
          <img
            src={hiresyncLogo}
            alt="Hiresync Logo"
            style={{ width: "100px", height: "100px", borderRadius: "12px" }}
          />
        </div>
        <h4 className="text-center mb-4 text-primary fw-bold">Giriş Paneli</h4>
        <form onSubmit={handleSubmit}>
          <div className="mb-3">
            <label htmlFor="username" className="form-label">Kullanıcı Adı</label>
            <div className="input-group">
              <span className="input-group-text"><i className="bi bi-person" /></span>
              <input
                type="text"
                id="username"
                className="form-control"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                placeholder="kullaniciadi"
              />
            </div>
          </div>

          <div className="mb-4">
            <label htmlFor="password" className="form-label">Şifre</label>
            <div className="input-group">
              <span className="input-group-text"><i className="bi bi-lock-fill" /></span>
              <input
                type="password"
                id="password"
                className="form-control"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="••••••"
              />
            </div>
          </div>

          <button type="submit" className="btn btn-primary w-100" disabled={isLoading}>
            {isLoading ? (
              <>
                <span className="spinner-border spinner-border-sm me-2" role="status" />
                Giriş Yapılıyor...
              </>
            ) : (
              "Giriş Yap"
            )}
          </button>
        </form>
        <div className="text-center mt-3">
          <a href="/forgot-password" className="text-light text-decoration-underline">Şifrenizi mi unuttunuz?</a>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
