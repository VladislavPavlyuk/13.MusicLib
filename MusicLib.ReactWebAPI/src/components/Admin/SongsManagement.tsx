import { useState } from 'react';
import { useSongs, useCreateSong, useUpdateSong, useDeleteSong } from '../../hooks/useSongs';
import { useGenres } from '../../hooks/useGenres';
import { useArtists } from '../../hooks/useArtists';
import type { Song } from '../../types';
import { confirmDelete, showSuccess, showError } from '../../utils/swal';
import { ErrorDisplay } from './ErrorDisplay';
import './Management.css';

export function SongsManagement() {
  const { data: songs, isLoading, error } = useSongs();
  const { data: genres } = useGenres();
  const { data: artists } = useArtists();
  const createMutation = useCreateSong();
  const updateMutation = useUpdateSong();
  const deleteMutation = useDeleteSong();

  const [editingSong, setEditingSong] = useState<Song | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState<Omit<Song, 'id'>>({
    title: '',
    release: '',
    youtubeLink: '',
    genreId: null,
    genre: null,
    artistId: null,
    artist: null,
    videoId: null,
    video: null,
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (editingSong) {
        await updateMutation.mutateAsync({ id: editingSong.id, song: { ...formData, id: editingSong.id } });
        showSuccess('Song updated successfully!');
      } else {
        await createMutation.mutateAsync(formData);
        showSuccess('Song created successfully!');
      }
      resetForm();
    } catch (err: any) {
      console.error('Error saving song:', err);
      showError('Error', err.response?.data?.error || 'Failed to save song');
    }
  };

  const handleEdit = (song: Song) => {
    setEditingSong(song);
    setFormData({
      title: song.title || '',
      release: song.release || '',
      youtubeLink: song.youtubeLink || '',
      genreId: song.genreId,
      genre: song.genre,
      artistId: song.artistId,
      artist: song.artist,
      videoId: song.videoId,
      video: song.video,
    });
    setShowForm(true);
  };

  const handleDelete = async (id: number) => {
    const song = songs?.find(s => s.id === id);
    const confirmed = await confirmDelete(
      'Delete Song?',
      `Are you sure you want to delete song "${song?.title}"? This action cannot be undone!`
    );
    
    if (!confirmed) return;
    
    try {
      await deleteMutation.mutateAsync(id);
      showSuccess('Song deleted successfully!');
    } catch (err: any) {
      console.error('Error deleting song:', err);
      showError('Error', err.response?.data?.error || 'Failed to delete song');
    }
  };

  const resetForm = () => {
    setEditingSong(null);
    setShowForm(false);
    setFormData({
      title: '',
      release: '',
      youtubeLink: '',
      genreId: null,
      genre: null,
      artistId: null,
      artist: null,
      videoId: null,
      video: null,
    });
  };

  if (isLoading) return <div className="loading">Loading songs...</div>;
  if (error) return <ErrorDisplay error={error} entityName="songs" />;

  return (
    <div className="management-container">
      <div className="management-header">
        <h2>Songs Management</h2>
        <button className="btn-primary" onClick={() => setShowForm(true)}>
          Add New Song
        </button>
      </div>

      {showForm && (
        <form className="management-form" onSubmit={handleSubmit}>
          <h3>{editingSong ? 'Edit Song' : 'Add New Song'}</h3>
          <div className="form-group">
            <label>Title:</label>
            <input
              type="text"
              value={formData.title}
              onChange={(e) => setFormData({ ...formData, title: e.target.value })}
              required
            />
          </div>
          <div className="form-group">
            <label>Release:</label>
            <input
              type="text"
              value={formData.release}
              onChange={(e) => setFormData({ ...formData, release: e.target.value })}
            />
          </div>
          <div className="form-group">
            <label>YouTube Link:</label>
            <input
              type="url"
              value={formData.youtubeLink}
              onChange={(e) => setFormData({ ...formData, youtubeLink: e.target.value })}
            />
          </div>
          <div className="form-group">
            <label>Genre ID:</label>
            <select
              value={formData.genreId || ''}
              onChange={(e) => setFormData({ ...formData, genreId: parseInt(e.target.value) || null })}
            >
              <option value="">Select Genre</option>
              {genres?.map((genre) => (
                <option key={genre.id} value={genre.id}>
                  {genre.title}
                </option>
              ))}
            </select>
          </div>
          <div className="form-group">
            <label>Artist ID:</label>
            <select
              value={formData.artistId || ''}
              onChange={(e) => setFormData({ ...formData, artistId: parseInt(e.target.value) || null })}
            >
              <option value="">Select Artist</option>
              {artists?.map((artist) => (
                <option key={artist.id} value={artist.id}>
                  {artist.name}
                </option>
              ))}
            </select>
          </div>
          <div className="form-actions">
            <button type="submit" className="btn-primary" disabled={createMutation.isPending || updateMutation.isPending}>
              {editingSong ? 'Update' : 'Create'}
            </button>
            <button type="button" className="btn-secondary" onClick={resetForm}>
              Cancel
            </button>
          </div>
        </form>
      )}

      <table className="management-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Title</th>
            <th>Artist</th>
            <th>Genre</th>
            <th>Release</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {songs?.map((song) => (
            <tr key={song.id}>
              <td>{song.id}</td>
              <td>{song.title}</td>
              <td>{song.artist || 'N/A'}</td>
              <td>{song.genre || 'N/A'}</td>
              <td>{song.release || 'N/A'}</td>
              <td>
                <button className="btn-edit" onClick={() => handleEdit(song)}>
                  Edit
                </button>
                <button className="btn-delete" onClick={() => handleDelete(song.id)}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
