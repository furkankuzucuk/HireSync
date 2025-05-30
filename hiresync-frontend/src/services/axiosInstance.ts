import axios from 'axios';

const axiosInstance = axios.create({
   baseURL: 'http://localhost:5065', // Gerekirse API base adresini ayarla
});

axiosInstance.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default axiosInstance;
