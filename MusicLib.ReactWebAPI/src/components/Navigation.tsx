import { Link, useLocation } from 'react-router-dom';
import './Navigation.css';

export function Navigation() {
  const location = useLocation();
  const isAdminRoute = location.pathname.startsWith('/admin');
  
  const isActive = (path: string) => {
    return location.pathname === path || location.pathname.startsWith(path + '/');
  };

  const isAdminActive = (path: string) => {
    return location.pathname === path;
  };

  if (isAdminRoute) {
    return (
      <>
        <header className="admin-header">
          <h1>Music Library Admin Panel</h1>
        </header>
        
        <nav className="admin-nav">
          <Link
            to="/admin"
            className={isAdminActive('/admin') ? 'admin-nav-link active' : 'admin-nav-link'}
          >
            Dashboard
          </Link>
          <Link
            to="/admin/users"
            className={isAdminActive('/admin/users') ? 'admin-nav-link active' : 'admin-nav-link'}
          >
            Users
          </Link>
          <Link
            to="/admin/songs"
            className={isAdminActive('/admin/songs') ? 'admin-nav-link active' : 'admin-nav-link'}
          >
            Songs
          </Link>
          <Link
            to="/admin/genres"
            className={isAdminActive('/admin/genres') ? 'admin-nav-link active' : 'admin-nav-link'}
          >
            Genres
          </Link>
        </nav>
      </>
    );
  }

  return (
    <nav className="navigation">
      <div className="nav-container">
        <Link to="/admin" className="nav-logo">
          Music Library
        </Link>
        
        <div className="nav-links">
          <Link 
            to="/admin" 
            className={isActive('/admin') ? 'nav-link active' : 'nav-link'}
          >
            Admin
          </Link>
        </div>
      </div>
    </nav>
  );
}
