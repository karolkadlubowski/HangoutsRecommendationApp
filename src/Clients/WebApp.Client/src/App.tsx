import { Route, Routes } from 'react-router-dom';
import './App.css';
import { CreatePlace } from './Components/CreatePlace/CreatePlace';
import FindPlace from './Components/FindPlace/FindPlace';
import FindVenue from './Components/FindVenue/FindVenue';
import MainPage from './Components/MainPage/MainPage';
import Profile from './Components/Profile/Profile';

function App() {
    return (
        <div>
            <Routes>
                <Route path="/" element={<MainPage />} />
                <Route path="/find" element={<FindPlace />} />
                <Route path="/findVenue" element={<FindVenue />} />
                <Route path="/create-place" element={<CreatePlace />} />
                <Route path="/profile" element={<Profile />} />
            </Routes>
        </div>
    );
}

export default App;
