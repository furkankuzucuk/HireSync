import React, { useState } from 'react';
import axios from 'axios'; // Import axios
import '../css/ForgotPassword.css';

const ForgotPassword = () => {
  const [email, setEmail] = useState('');
  const [sent, setSent] = useState(false);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false); // For showing loading state

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    // Validate email input
    if (!email) {
      setError("Lütfen geçerli bir e-posta adresi girin.");
      return;
    }

    setLoading(true); // Start loading indicator
    setError(''); // Clear previous errors

    try {
      // Make API call to the back-end
      const response = await axios.post('/api/forgot-password', { email });

      // Handle successful response
      if (response.status === 200) {
        setSent(true); // Set success state to show success message
      } else {
        setError("Beklenmedik bir hata oluştu. Lütfen tekrar deneyin.");
      }
    } catch (err) {
      // Handle error response
      setError("Bir hata oluştu. Lütfen tekrar deneyin.");
    } finally {
      setLoading(false); // Stop loading indicator
    }
  };

  return (
    <div className="forgot-password-container">
      <div className="forgot-password-card">
        <h2>🔐 Şifre Sıfırlama</h2>
        {sent ? (
          <p className="success-message">Sıfırlama bağlantısı e-postanıza gönderildi.</p>
        ) : (
          <form onSubmit={handleSubmit}>
            <div className="input-group">
              <label htmlFor="email">E-Posta</label>
              <input
                type="email"
                id="email"
                value={email}
                onChange={(e) => {
                  setEmail(e.target.value);
                  setError(''); // Clear error message when user types
                }}
                required
                className="input-field"
                placeholder="E-posta adresinizi girin"
              />
              {error && <p className="error-message">{error}</p>}
            </div>
            <button type="submit" className="submit-button" disabled={loading}>
              {loading ? 'Gönderiliyor...' : 'Bağlantı Gönder'}
            </button>
          </form>
        )}
      </div>
    </div>
  );
};

export default ForgotPassword;
