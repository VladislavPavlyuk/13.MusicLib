import axios from 'axios';
import type { Song, Artist, Genre, User } from '../types';

// This is where our backend server is running
const API_BASE_URL = import.meta.env.VITE_API_URL || 'https://localhost:7169/api';

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Show errors in console when something goes wrong
apiClient.interceptors.response.use(
  (response) => {
    console.log('API Success:', {
      status: response.status,
      url: response.config?.url,
      data: response.data,
    });
    return response;
  },
  (error) => {
    if (error.response) {
      // Server said no
      console.error('API Error Response:', {
        status: error.response.status,
        statusText: error.response.statusText,
        data: error.response.data,
        url: error.config?.url,
        fullUrl: `${API_BASE_URL}${error.config?.url}`,
        headers: error.response.headers,
      });
    } else if (error.request) {
      // We sent request but got nothing back
      console.error('Network Error (No Response):', {
        message: error.message,
        url: error.config?.url,
        method: error.config?.method,
        baseURL: API_BASE_URL,
        fullUrl: `${API_BASE_URL}${error.config?.url}`,
        code: error.code,
        request: error.request,
      });
    } else {
      // Something broke before we could send request
      console.error('Request Setup Error:', error.message);
    }
    return Promise.reject(error);
  }
);

// Log every request we make
apiClient.interceptors.request.use(
  (config) => {
    console.log('API Request:', {
      method: config.method?.toUpperCase(),
      url: config.url,
      baseURL: config.baseURL,
      fullUrl: `${config.baseURL}${config.url}`,
    });
    return config;
  },
  (error) => {
    console.error('Request Interceptor Error:', error);
    return Promise.reject(error);
  }
);

// Check if API is reachable
export const checkApiConnection = async (): Promise<boolean> => {
  try {
    const response = await apiClient.get('/HealthApi');
    console.log('API Connection OK:', response.data);
    return true;
  } catch (error) {
    console.error('API Connection Failed:', error);
    return false;
  }
};

export const songsApi = {
  getAll: async (sortOrder?: string): Promise<Song[]> => {
    const params = sortOrder ? { sortOrder } : {};
    const response = await apiClient.get<Song[]>('/SongsApi', { params });
    return response.data;
  },

  getById: async (id: number): Promise<Song> => {
    const response = await apiClient.get<Song>(`/SongsApi/${id}`);
    return response.data;
  },

  create: async (song: Omit<Song, 'id'>): Promise<Song> => {
    const response = await apiClient.post<Song>('/SongsApi', song);
    return response.data;
  },

  update: async (id: number, song: Song): Promise<void> => {
    await apiClient.put(`/SongsApi/${id}`, song);
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/SongsApi/${id}`);
  },
};

export const artistsApi = {
  getAll: async (): Promise<Artist[]> => {
    const response = await apiClient.get<Artist[]>('/ArtistsApi');
    return response.data;
  },

  getById: async (id: number): Promise<Artist> => {
    const response = await apiClient.get<Artist>(`/ArtistsApi/${id}`);
    return response.data;
  },

  create: async (artist: Omit<Artist, 'id'>): Promise<Artist> => {
    const response = await apiClient.post<Artist>('/ArtistsApi', artist);
    return response.data;
  },

  update: async (id: number, artist: Artist): Promise<void> => {
    await apiClient.put(`/ArtistsApi/${id}`, artist);
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/ArtistsApi/${id}`);
  },
};

export const genresApi = {
  getAll: async (): Promise<Genre[]> => {
    const response = await apiClient.get<Genre[]>('/GenresApi');
    return response.data;
  },

  getById: async (id: number): Promise<Genre> => {
    const response = await apiClient.get<Genre>(`/GenresApi/${id}`);
    return response.data;
  },

  create: async (genre: Omit<Genre, 'id'>): Promise<Genre> => {
    const response = await apiClient.post<Genre>('/GenresApi', genre);
    return response.data;
  },

  update: async (id: number, genre: Genre): Promise<void> => {
    await apiClient.put(`/GenresApi/${id}`, genre);
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/GenresApi/${id}`);
  },
};

export const usersApi = {
  getAll: async (): Promise<User[]> => {
    const response = await apiClient.get<User[]>('/UsersApi');
    return response.data;
  },

  getById: async (id: number): Promise<User> => {
    const response = await apiClient.get<User>(`/UsersApi/${id}`);
    return response.data;
  },

  create: async (user: Omit<User, 'id'>): Promise<User> => {
    const response = await apiClient.post<User>('/UsersApi', user);
    return response.data;
  },

  update: async (id: number, user: User): Promise<void> => {
    await apiClient.put(`/UsersApi/${id}`, user);
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/UsersApi/${id}`);
  },
};
