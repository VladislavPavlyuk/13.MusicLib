using MusicLib.BLL.DTO;
using MusicLib.DAL.Entities;
using MusicLib.DAL.Interfaces;
using MusicLib.BLL.Infrastructure;
using MusicLib.BLL.Interfaces;
using AutoMapper;

namespace MusicLib.BLL.Services
{
    public class ArtistService : IArtistService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;

        public ArtistService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task CreateArtist(ArtistDTO artistDto)
        {
            var artist = new Artist
            {
                Id = artistDto.Id,
                Name = artistDto.Name,
                BirthDate = artistDto.BirthDate

            };
            await Database.Artists.Create(artist);

            await Database.Save();
        }

        public async Task UpdateArtist(ArtistDTO artistDto)
        {
            var artist = new Artist
            {
                Id = artistDto.Id,
                Name = artistDto.Name,
                BirthDate = artistDto.BirthDate
            };
            Database.Artists.Update(artist);

            await Database.Save();
        }

        public async Task DeleteArtist(int id)
        {
            try 
            { 
                await Database.Artists.Delete(id);

                await Database.Save();            
            } 
            catch (Exception ex)
            {

            }
}

        public async Task<ArtistDTO> GetArtist(int id)
        {
            var artist = await Database.Artists.Get(id);

            if (artist == null)
                throw new ValidationException("Wrong artist!", "");

            return new ArtistDTO
            {
                Id = artist.Id,
                Name = artist.Name,
                BirthDate = artist.BirthDate
            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.
        public async Task<IEnumerable<ArtistDTO>> GetArtists()
        {
            var artists = await Database.Artists.GetAll();
            return _mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistDTO>>(artists);
        }

        public async Task<IEnumerable<ArtistDTO>> GetSortedItemsAsync(string sortOrder)
        {
            return (IEnumerable<ArtistDTO>)await Database.Artists.GetSortedAsync(sortOrder);
        }
    }
}
