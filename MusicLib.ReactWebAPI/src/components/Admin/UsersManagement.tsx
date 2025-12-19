import { useState } from 'react';
import { useUsers, useCreateUser, useUpdateUser, useDeleteUser } from '../../hooks/useUsers';
import type { User } from '../../types';
import { confirmDelete, showSuccess, showError } from '../../utils/swal';
import { ErrorDisplay } from './ErrorDisplay';
import './Management.css';

export function UsersManagement() {
  const { data: users, isLoading, error } = useUsers();
  const createMutation = useCreateUser();
  const updateMutation = useUpdateUser();
  const deleteMutation = useDeleteUser();

  const [editingUser, setEditingUser] = useState<User | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState<Omit<User, 'id'>>({
    email: '',
    password: '',
    roleId: null,
    role: null,
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (editingUser) {
        await updateMutation.mutateAsync({ id: editingUser.id, user: { ...formData, id: editingUser.id } });
        showSuccess('User updated successfully!');
      } else {
        await createMutation.mutateAsync(formData);
        showSuccess('User created successfully!');
      }
      resetForm();
    } catch (err: any) {
      console.error('Error saving user:', err);
      showError('Error', err.response?.data?.error || 'Failed to save user');
    }
  };

  const handleEdit = (user: User) => {
    setEditingUser(user);
    setFormData({
      email: user.email || '',
      password: '',
      roleId: user.roleId,
      role: user.role,
    });
    setShowForm(true);
  };

  const handleDelete = async (id: number) => {
    const user = users?.find(u => u.id === id);
    const confirmed = await confirmDelete(
      'Delete User?',
      `Are you sure you want to delete user "${user?.email}"? This action cannot be undone!`
    );
    
    if (!confirmed) return;
    
    try {
      await deleteMutation.mutateAsync(id);
      showSuccess('User deleted successfully!');
    } catch (err: any) {
      console.error('Error deleting user:', err);
      showError('Error', err.response?.data?.error || 'Failed to delete user');
    }
  };

  const resetForm = () => {
    setEditingUser(null);
    setShowForm(false);
    setFormData({ email: '', password: '', roleId: null, role: null });
  };

  if (isLoading) return <div className="loading">Loading users...</div>;
  if (error) return <ErrorDisplay error={error} entityName="users" />;

  return (
    <div className="management-container">
      <div className="management-header">
        <h2>Users Management</h2>
        <button className="btn-primary" onClick={() => setShowForm(true)}>
          Add New User
        </button>
      </div>

      {showForm && (
        <form className="management-form" onSubmit={handleSubmit}>
          <h3>{editingUser ? 'Edit User' : 'Add New User'}</h3>
          <div className="form-group">
            <label>Email:</label>
            <input
              type="email"
              value={formData.email}
              onChange={(e) => setFormData({ ...formData, email: e.target.value })}
              required
            />
          </div>
          <div className="form-group">
            <label>Password:</label>
            <input
              type="password"
              value={formData.password}
              onChange={(e) => setFormData({ ...formData, password: e.target.value })}
              required={!editingUser}
            />
          </div>
          <div className="form-group">
            <label>Role ID:</label>
            <input
              type="number"
              value={formData.roleId || ''}
              onChange={(e) => setFormData({ ...formData, roleId: parseInt(e.target.value) || null })}
            />
          </div>
          <div className="form-actions">
            <button type="submit" className="btn-primary" disabled={createMutation.isPending || updateMutation.isPending}>
              {editingUser ? 'Update' : 'Create'}
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
            <th>Email</th>
            <th>Role</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {users?.map((user) => (
            <tr key={user.id}>
              <td>{user.id}</td>
              <td>{user.email}</td>
              <td>{user.role || 'N/A'}</td>
              <td>
                <button className="btn-edit" onClick={() => handleEdit(user)}>
                  Edit
                </button>
                <button className="btn-delete" onClick={() => handleDelete(user.id)}>
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
