import { useState } from 'react';
import { useGenres, useCreateGenre, useUpdateGenre, useDeleteGenre } from '../../hooks/useGenres';
import type { Genre } from '../../types';
import { confirmDelete, showSuccess, showError } from '../../utils/swal';
import { ErrorDisplay } from './ErrorDisplay';
import './Management.css';

export function GenresManagement() {
  const { data: genres, isLoading, error } = useGenres();
  const createMutation = useCreateGenre();
  const updateMutation = useUpdateGenre();
  const deleteMutation = useDeleteGenre();

  const [editingGenre, setEditingGenre] = useState<Genre | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState<Omit<Genre, 'id'>>({
    title: '',
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (editingGenre) {
        await updateMutation.mutateAsync({ id: editingGenre.id, genre: { ...formData, id: editingGenre.id } });
        showSuccess('Genre updated successfully!');
      } else {
        await createMutation.mutateAsync(formData);
        showSuccess('Genre created successfully!');
      }
      resetForm();
    } catch (err: unknown) {
      console.error('Error saving genre:', err);
      const errorMessage = (err as { response?: { data?: { error?: string } } })?.response?.data?.error || 'Failed to save genre';
      showError('Error', errorMessage);
    }
  };

  const handleEdit = (genre: Genre) => {
    setEditingGenre(genre);
    setFormData({ title: genre.title || '' });
    setShowForm(true);
  };

  const handleDelete = async (id: number) => {
    const genre = genres?.find(g => g.id === id);
    const confirmed = await confirmDelete(
      'Delete Genre?',
      `Are you sure you want to delete genre "${genre?.title}"? This action cannot be undone!`
    );
    
    if (!confirmed) return;
    
    try {
      await deleteMutation.mutateAsync(id);
      showSuccess('Genre deleted successfully!');
    } catch (err: unknown) {
      console.error('Error deleting genre:', err);
      const errorMessage = (err as { response?: { data?: { error?: string } } })?.response?.data?.error || 'Failed to delete genre';
      showError('Error', errorMessage);
    }
  };

  const resetForm = () => {
    setEditingGenre(null);
    setShowForm(false);
    setFormData({ title: '' });
  };

  if (isLoading) return <div className="loading">Loading genres...</div>;
  if (error) return <ErrorDisplay error={error} entityName="genres" />;

  return (
    <div className="management-container">
      <div className="management-header">
        <h2>Genres</h2>        
        <button className="btn-primary" onClick={() => setShowForm(true)}>
          Add New Genre
        </button>
      </div>

      {showForm && (
        <form className="management-form" onSubmit={handleSubmit}>          
          <h3>{editingGenre ? 'Edit Genre' : 'Add New Genre'}</h3>
          <div className="form-group">
            <label>Title:</label>
            <input
              type="text"
              value={formData.title || ''}
              onChange={(e) => setFormData({ ...formData, title: e.target.value })}
              required
            />
          </div>
          <div className="form-actions">
            <button type="submit" className="btn-primary" disabled={createMutation.isPending || updateMutation.isPending}>
              {editingGenre ? 'Update' : 'Create'}
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
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {genres?.map((genre) => (
            <tr key={genre.id}>
              <td>{genre.id}</td>
              <td>{genre.title}</td>
              <td>
                <button className="btn-edit" onClick={() => handleEdit(genre)}>
                  Edit
                </button>
                <button className="btn-delete" onClick={() => handleDelete(genre.id)}>
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
