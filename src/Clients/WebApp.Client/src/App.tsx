import { Route, Routes } from 'react-router-dom';
import './App.css';
import FindPlace from './Components/FindPlace/FindPlace';
import MainPage from './Components/MainPage/MainPage';
import Profile from './Components/Profile/Profile';

function App() {
  return (
    <div >
     <Routes>
      <Route path="/" element={<MainPage />} />
      <Route path="/find" element={<FindPlace />} />
      <Route path="/profile" element={<Profile />} />
     </Routes>
    </div>
  );
}

export default App;
