using MusicLib.BLL.DTO;
using MusicLib.DAL.Entities;
using MusicLib.DAL.Interfaces;
using MusicLib.BLL.Infrastructure;
using MusicLib.BLL.Interfaces;
using AutoMapper;

namespace MusicLib.BLL.Services
{
    public class SongService : ISongService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;

        public SongService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task CreateSong(SongDTO songDto)
        {
            var song = new Song
            {
                Id = songDto.Id,
                Title = songDto.Title,
                Release = songDto.Release,
                GenreId = songDto.GenreId,
                ArtistId = songDto.ArtistId,
                AudioId = songDto.AudioId
            };
            await Database.Songs.Create(song);

            await Database.Save();
        }

        public async Task UpdateSong(SongDTO songDto)
        {
            var song = new Song
            {
                Id = songDto.Id,
                Title = songDto.Title,
                Release = songDto.Release,
                GenreId = songDto.GenreId,
                ArtistId = songDto.ArtistId,
                AudioId = songDto.AudioId
            };
            Database.Songs.Update(song);

            await Database.Save();
        }

        public async Task DeleteSong(int id)
        {
            await Database.Songs.Delete(id);

            await Database.Save();
        }

        public async Task<SongDTO> GetSong(int id)
        {
            var song = await Database.Songs.Get(id);

            if (song == null)
                throw new ValidationException("Wrong song!", "");

            return new SongDTO
            {
                Id = song.Id,
                Title = song.Title,
                Release = song.Release,
                GenreId = song.GenreId,
                Genre = song.Genre?.Title,
                ArtistId = song.ArtistId,
                Artist = song.Artist?.Name,
                AudioId = song.AudioId,
                Audio = song.Audio?.FileName
            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.

        public async Task<IEnumerable<SongDTO>> GetSongs()
        {
            var songs = await Database.Songs.GetAll();
            return _mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(songs);
        }

        public async Task<IEnumerable<SongDTO>> GetSortedItemsAsync(string sortOrder)
        {
            var songs = await Database.Songs.GetSortedAsync(sortOrder);
            return _mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(songs);
        }
    }
}
