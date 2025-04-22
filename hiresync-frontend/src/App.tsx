import React from 'react';
import LoginPage from './pages/LoginPage'; // LoginPage bileşenini dahil et

const App = () => {
  return (
    <div className="App">
      <LoginPage /> {/* LoginPage'i burada çağır */}
    </div>
  );
};

export default App;

