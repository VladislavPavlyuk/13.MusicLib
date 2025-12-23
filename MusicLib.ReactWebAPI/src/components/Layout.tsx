import type { ReactNode } from 'react';
import { Outlet, useLocation } from 'react-router-dom';
import { Navigation } from './Navigation';
import './Layout.css';

interface LayoutProps {
  children?: ReactNode;
}

export function Layout({ children }: LayoutProps) {
  const location = useLocation();
  const isAdminRoute = location.pathname.startsWith('/admin');

  return (
    <div className="layout">
      <Navigation />
      {isAdminRoute ? (
        <div className="admin-content">
          <Outlet />
        </div>
      ) : (
        <main className="layout-content">
          {children}
        </main>
      )}
    </div>
  );
}
