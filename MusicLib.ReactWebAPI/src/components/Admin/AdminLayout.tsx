import { useState } from 'react';
import { UsersManagement } from './UsersManagement';
import { SongsManagement } from './SongsManagement';
import { GenresManagement } from './GenresManagement';
import './AdminLayout.css';

type Tab = 'users' | 'songs' | 'genres';

export function AdminLayout() {
  const [activeTab, setActiveTab] = useState<Tab>('users');

  return (
    <div className="admin-layout">
      <header className="admin-header">
        <h1>MusicLib Admin Panel</h1>
      </header>
      
      <nav className="admin-nav">
        <button
          className={activeTab === 'users' ? 'active' : ''}
          onClick={() => setActiveTab('users')}
        >
          Users
        </button>
        <button
          className={activeTab === 'songs' ? 'active' : ''}
          onClick={() => setActiveTab('songs')}
        >
          Songs
        </button>
        <button
          className={activeTab === 'genres' ? 'active' : ''}
          onClick={() => setActiveTab('genres')}
        >
          Genres
        </button>
      </nav>

      <main className="admin-content">
        {activeTab === 'users' && <UsersManagement />}
        {activeTab === 'songs' && <SongsManagement />}
        {activeTab === 'genres' && <GenresManagement />}
      </main>
    </div>
  );
}
