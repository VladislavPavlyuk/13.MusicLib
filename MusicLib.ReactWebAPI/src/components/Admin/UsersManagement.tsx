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
  const [formData, setFormData] = useState<Omit<User, 'id' | 'password'>>({
    email: '',
    roleId: null,
    role: null,
  });
  const [password, setPassword] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    // Check if email and password are correct when creating new user
    if (!editingUser) {
      const emailValue = formData.email?.trim() || '';
      const passwordValue = password.trim();
      
      if (!emailValue) {
        showError('Validation Error', 'Email is required');
        return;
      }
      
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailRegex.test(emailValue)) {
        showError('Validation Error', 'Please enter a valid email address');
        return;
      }
      
      if (!passwordValue) {
        showError('Validation Error', 'Password is required');
        return;
      }
      
      if (passwordValue.length < 6) {
        showError('Validation Error', 'Password must be at least 6 characters long');
        return;
      }
    }
    
    try {
      if (editingUser) {
        await updateMutation.mutateAsync({ 
          id: editingUser.id, 
          user: { 
            id: editingUser.id,
            email: formData.email,
            roleId: formData.roleId,
            password: editingUser.password,
            salt: editingUser.salt,
            role: editingUser.role,
          }
        });
        showSuccess('User updated successfully!');
      } else {
        await createMutation.mutateAsync({ ...formData, password });
        showSuccess('User created successfully!');
      }
      resetForm();
    } catch (err: unknown) {
      console.error('Error saving user:', err);
      const errorMessage = (err as { response?: { data?: { error?: string } } })?.response?.data?.error || 'Failed to save user';
      showError('Error', errorMessage);
    }
  };

  const handleEdit = (user: User) => {
    setEditingUser(user);
    setFormData({
      email: user.email || '',
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
    } catch (err: unknown) {
      console.error('Error deleting user:', err);
      const errorMessage = (err as { response?: { data?: { error?: string } } })?.response?.data?.error || 'Failed to delete user';
      showError('Error', errorMessage);
    }
  };

  const resetForm = () => {
    setEditingUser(null);
    setShowForm(false);
    setFormData({ email: '', roleId: null, role: null });
    setPassword('');
  };

  if (isLoading) return <div className="loading">Loading users...</div>;
  if (error) return <ErrorDisplay error={error} entityName="users" />;

  return (
    <div className="management-container">
      <div className="management-header">
        <h2>Users</h2>
        <button className="btn-primary" onClick={() => setShowForm(true)}>
          Add New User
        </button>
      </div>

      {showForm && (
        <form className="management-form" onSubmit={handleSubmit} noValidate>
          <h3>{editingUser ? 'Edit User' : 'Add New User'}</h3>
          <div className="form-group">
            <label>Email:</label>
            <input
              type="email"
              value={formData.email || ''}
              onChange={(e) => setFormData({ ...formData, email: e.target.value })}
            />
          </div>
          {!editingUser && (
            <div className="form-group">
              <label>Password:</label>
              <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>
          )}
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
