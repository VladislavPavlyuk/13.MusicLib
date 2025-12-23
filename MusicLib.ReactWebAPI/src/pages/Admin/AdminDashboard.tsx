import { Link } from 'react-router-dom';
import './AdminDashboard.css';

export function AdminDashboard() {
  return (
    <div className="admin-dashboard">
      <h1>Admin Dashboard</h1>
      <p>Select a section to manage:</p>
      
      <div className="admin-links">
        <Link to="/admin/users" className="admin-link-card">
          <h2>Users</h2>
          <p>Manage user accounts</p>
        </Link>
        
        <Link to="/admin/songs" className="admin-link-card">
          <h2>Songs</h2>
          <p>Manage songs collection</p>
        </Link>
        
        <Link to="/admin/genres" className="admin-link-card">
          <h2>Genres</h2>
          <p>Manage music genres</p>
        </Link>
      </div>
    </div>
  );
}

