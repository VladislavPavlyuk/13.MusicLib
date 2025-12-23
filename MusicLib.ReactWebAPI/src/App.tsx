import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { Layout } from './components/Layout';
import { AdminDashboard } from './pages/Admin/AdminDashboard';
import { UsersPage } from './pages/Admin/UsersPage';
import { SongsPage } from './pages/Admin/SongsPage';
import { GenresPage } from './pages/Admin/GenresPage';
import './App.css';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/admin" replace />} />
        <Route path="admin" element={<Layout />}>
          <Route index element={<AdminDashboard />} />
          <Route path="users" element={<UsersPage />} />
          <Route path="songs" element={<SongsPage />} />
          <Route path="genres" element={<GenresPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
