// src/pages/useLogout.ts
import { useNavigate } from 'react-router-dom';

const useLogout = () => {
  const navigate = useNavigate();

  const logout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('username');
    localStorage.removeItem('userId'); // ✅ userId temizlendi
    localStorage.removeItem('tokenExpiration');
    navigate('/', { replace: true });
  };

  return logout;
};

export default useLogout;
