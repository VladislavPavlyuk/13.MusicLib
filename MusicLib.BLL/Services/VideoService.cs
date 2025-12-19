using MusicLib.BLL.DTO;
using MusicLib.DAL.Entities;
using MusicLib.DAL.Interfaces;
using MusicLib.BLL.Infrastructure;
using MusicLib.BLL.Interfaces;
using AutoMapper;

namespace MusicLib.BLL.Services
{
    public class AudioService : IAudioService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;

        public AudioService(IUnitOfWork uow, IMapper mapper)
        {
            Database = uow;
            _mapper = mapper;
        }

        public async Task CreateAudio(AudioDTO audioDto)
        {
            var audio = new Audio
            {
                Id = audioDto.Id,
                FileName = audioDto.FileName,
                Path = audioDto.Path
            };
            await Database.Audios.Create(audio);

            await Database.Save();
        }

        public async Task UpdateAudio(AudioDTO audioDto)
        {
            var audio = new Audio
            {
                Id = audioDto.Id,
                FileName = audioDto.FileName,
                Path = audioDto.Path
            };
            Database.Audios.Update(audio);

            await Database.Save();
        }

        public async Task DeleteAudio(int id)
        {
            try
            {
                await Database.Audios.Delete(id);
                
                await Database.Save();

            } catch (Exception ex)
            {

            }

        }

        public async Task<AudioDTO> GetAudio(int id)
        {
            var audio = await Database.Audios.Get(id);

            if (audio == null)
                throw new ValidationException("Wrong audio!", "");

            return new AudioDTO
            {
                Id = audio.Id,
                FileName = audio.FileName,
                Path = audio.Path
            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.
        public async Task<IEnumerable<AudioDTO>> GetAudios()
        {
            var audios = await Database.Audios.GetAll();
            return _mapper.Map<IEnumerable<Audio>, IEnumerable<AudioDTO>>(audios);
        }

        public async Task<IEnumerable<AudioDTO>> GetSortedItemsAsync(string sortOrder)
        {
            var audios = await Database.Audios.GetSortedAsync(sortOrder);
            return _mapper.Map<IEnumerable<Audio>, IEnumerable<AudioDTO>>(audios);
        }
    }
}
