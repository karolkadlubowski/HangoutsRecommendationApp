import { Toaster } from 'react-hot-toast';
import { Navigate, Route, Routes } from 'react-router-dom';
import './App.css';
import Signin from './Components/Authorization/Signin';
import Signup from './Components/Authorization/Signup';
import { CreatePlace } from './Components/CreatePlace/CreatePlace';
import Favourites from './Components/Favourites/Favourites';
import FindPlace from './Components/FindPlace/FindPlace';
import FindVenue from './Components/FindVenue/FindVenue';
import './Styles/Styles.scss';

function App() {
    const ProtectedRoute = ({ children }: any) => {
        const token = localStorage.getItem('token');

        if (!token) {
            return <Navigate to="/signin" replace />;
        }

        return children;
    };

    return (
        <div>
            <Routes>
                <Route
                    path="/"
                    element={
                        <ProtectedRoute>
                            <FindPlace />
                        </ProtectedRoute>
                    }
                />
                <Route
                    path="/findVenue"
                    element={
                        <ProtectedRoute>
                            <FindVenue />
                        </ProtectedRoute>
                    }
                />
                <Route
                    path="/create-place"
                    element={
                        <ProtectedRoute>
                            <CreatePlace />
                        </ProtectedRoute>
                    }
                />
                <Route
                    path="/favourites"
                    element={
                        <ProtectedRoute>
                            <Favourites />
                        </ProtectedRoute>
                    }
                />
                <Route path="/signup" element={<Signup />} />
                <Route path="/signin" element={<Signin />} />
            </Routes>
            <Toaster />
        </div>
    );
}

export default App;
