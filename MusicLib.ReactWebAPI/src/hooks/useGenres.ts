import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { genresApi } from '../services/api';
import type { Genre } from '../types';

export const useGenres = () => {
  return useQuery({
    queryKey: ['genres'],
    queryFn: async () => {
      try {
        const data = await genresApi.getAll();
        console.log('useGenres: Received data', data);
        return data;
      } catch (error) {
        console.error('useGenres: Error in queryFn', error);
        throw error;
      }
    },
    retry: 1,
    retryDelay: 1000,
  });
};

export const useGenre = (id: number) => {
  return useQuery({
    queryKey: ['genre', id],
    queryFn: () => genresApi.getById(id),
    enabled: !!id,
  });
};

export const useCreateGenre = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: (genre: Omit<Genre, 'id'>) => genresApi.create(genre),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['genres'] });
    },
  });
};

export const useUpdateGenre = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: ({ id, genre }: { id: number; genre: Genre }) => genresApi.update(id, genre),
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries({ queryKey: ['genres'] });
      queryClient.invalidateQueries({ queryKey: ['genre', variables.id] });
    },
  });
};

export const useDeleteGenre = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: (id: number) => genresApi.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['genres'] });
    },
  });
};
