import { useState, useEffect } from 'react';
import { songsApi } from '../services/api';
import type { Song } from '../types';
import './SongsList.css';

export function SongsList() {
  const [songs, setSongs] = useState<Song[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [sortOrder, setSortOrder] = useState<string>('');

  useEffect(() => {
    loadSongs();
  }, [sortOrder]);

  const loadSongs = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await songsApi.getAll(sortOrder || undefined);
      setSongs(data);
    } catch (err: any) {
      let errorMessage = 'Failed to load songs';
      
      if (err.response) {
        // Server returned an error
        errorMessage = `Server error: ${err.response.status} - ${err.response.statusText}`;
        if (err.response.data?.error) {
          errorMessage += ` - ${err.response.data.error}`;
        }
      } else if (err.request) {
        // No response from server
        errorMessage = 'Network error: Could not connect to server. Make sure the backend is running.';
      } else {
        // Request setup error
        errorMessage = `Request error: ${err.message}`;
      }
      
      setError(errorMessage);
      console.error('Error loading songs:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: number) => {
    if (!confirm('Are you sure you want to delete this song?')) {
      return;
    }
    try {
      await songsApi.delete(id);
      await loadSongs();
    } catch (err) {
      setError('Failed to delete song');
      console.error(err);
    }
  };

  if (loading) return <div className="loading">Loading...</div>;
  if (error) return <div className="error">Error: {error}</div>;

  return (
    <div className="songs-container">
      <div className="songs-header">
        <h1>Music Library</h1>
        <div className="sort-controls">
          <label>
            Sort by:
            <select value={sortOrder} onChange={(e) => setSortOrder(e.target.value)}>
              <option value="">Default</option>
              <option value="SongTitleDesc">Title (Desc)</option>
              <option value="ArtistNameAsc">Artist (Asc)</option>
              <option value="ArtistNameDesc">Artist (Desc)</option>
              <option value="GenreTitleAsc">Genre (Asc)</option>
              <option value="GenreTitleDesc">Genre (Desc)</option>
              <option value="SongReleaseDateAsc">Release Date (Asc)</option>
              <option value="SongReleaseDateDesc">Release Date (Desc)</option>
            </select>
          </label>
        </div>
      </div>

      <div className="songs-grid">
        {songs.map((song) => (
          <div key={song.id} className="song-card">
            <h3>{song.title || 'Untitled'}</h3>
            <div className="song-info">
              <p><strong>Artist:</strong> {song.artist || 'Unknown'}</p>
              <p><strong>Genre:</strong> {song.genre || 'Unknown'}</p>
              <p><strong>Release:</strong> {song.release || 'Unknown'}</p>
            </div>
            {song.youtubeLink && (
              <a
                href={song.youtubeLink}
                target="_blank"
                rel="noopener noreferrer"
                className="youtube-link"
              >
                Watch on YouTube
              </a>
            )}
            <button
              onClick={() => handleDelete(song.id)}
              className="delete-btn"
            >
              Delete
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}
