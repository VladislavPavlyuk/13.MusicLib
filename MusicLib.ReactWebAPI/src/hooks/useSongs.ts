import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { songsApi } from '../services/api';
import type { Song } from '../types';

export const useSongs = (sortOrder?: string) => {
  return useQuery({
    queryKey: ['songs', sortOrder],
    queryFn: async () => {
      try {
        const data = await songsApi.getAll(sortOrder);
        console.log('useSongs: Received data', data);
        return data;
      } catch (error) {
        console.error('useSongs: Error in queryFn', error);
        throw error;
      }
    },
    retry: 1,
    retryDelay: 1000,
  });
};

export const useSong = (id: number) => {
  return useQuery({
    queryKey: ['song', id],
    queryFn: () => songsApi.getById(id),
    enabled: !!id,
  });
};

export const useCreateSong = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: (song: Omit<Song, 'id'>) => songsApi.create(song),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['songs'] });
    },
  });
};

export const useUpdateSong = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: ({ id, song }: { id: number; song: Song }) => songsApi.update(id, song),
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries({ queryKey: ['songs'] });
      queryClient.invalidateQueries({ queryKey: ['song', variables.id] });
    },
  });
};

export const useDeleteSong = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: (id: number) => songsApi.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['songs'] });
    },
  });
};
