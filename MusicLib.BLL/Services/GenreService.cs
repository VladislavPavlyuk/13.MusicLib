using MusicLib.BLL.DTO;
using MusicLib.DAL.Entities;
using MusicLib.DAL.Interfaces;
using MusicLib.BLL.Infrastructure;
using MusicLib.BLL.Interfaces;
using AutoMapper;

namespace MusicLib.BLL.Services
{
    public class GenreService: IGenreService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task CreateGenre(GenreDTO genreDto)
        {
            var genre = new Genre
            {
                Id = genreDto.Id,
                Title = genreDto.Title
            };
            await Database.Genres.Create(genre);

            await Database.Save();
        }

        public async Task UpdateGenre(GenreDTO genreDto)
        {
            var genre = new Genre
            {
                Id = genreDto.Id,
                Title = genreDto.Title,
            };
            Database.Genres.Update(genre);

            await Database.Save();
        }

        public async Task DeleteGenre(int id)
        {
            await Database.Genres.Delete(id);
            await Database.Save();
            /*
            try {            
                await Database.Genres.Delete(id);
                await Database.Save();
            } catch (Exception ex)
            { }*/
        }
        public async Task<GenreDTO> GetGenre(int id)
        {
            var genre = await Database.Genres.Get(id);

            if (genre == null)
                throw new ValidationException("Wrong genre!", "");

            return new GenreDTO
            {
                Id = genre.Id,
                Title = genre.Title
            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.
        public async Task<IEnumerable<GenreDTO>> GetGenres()
        {
            var genres = await Database.Genres.GetAll();
            return _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(genres);
        }

        public async Task<IEnumerable<GenreDTO>> GetSortedItemsAsync(string sortOrder)
        {
            return (IEnumerable<GenreDTO>)await Database.Genres.GetSortedAsync(sortOrder);
        }

    }
}
