export interface Song {
  id: number;
  title: string | null;
  release: string | null;
  youtubeLink: string | null;
  genreId: number | null;
  genre: string | null;
  artistId: number | null;
  artist: string | null;
  videoId: number | null;
  video: string | null;
}

export interface Artist {
  id: number;
  name: string | null;
  birthDate: string | null;
}

export interface Genre {
  id: number;
  title: string | null;
}

export interface User {
  id: number;
  email: string | null;
  password?: string | null;
  salt?: string | null;
  roleId: number | null;
  role: string | null;
}
