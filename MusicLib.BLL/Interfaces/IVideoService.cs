using MusicLib.BLL.DTO;

namespace MusicLib.BLL.Interfaces
{
    public interface IAudioService
    {
        Task CreateAudio(AudioDTO audioDto);
        Task UpdateAudio(AudioDTO audioDto);
        Task DeleteAudio(int id);
        Task<AudioDTO> GetAudio(int id);
        Task<IEnumerable<AudioDTO>> GetAudios();
        Task<IEnumerable<AudioDTO>> GetSortedItemsAsync(string sortOrder);
    }
}
