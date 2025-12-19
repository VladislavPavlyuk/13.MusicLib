import { useQuery } from '@tanstack/react-query';
import { artistsApi } from '../services/api';

export const useArtists = () => {
  return useQuery({
    queryKey: ['artists'],
    queryFn: () => artistsApi.getAll(),
  });
};
