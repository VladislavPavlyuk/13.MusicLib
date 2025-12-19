using Microsoft.AspNetCore.Mvc.Rendering;
using MusicLib.BLL.DTO;

namespace MusicLib.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(
            List<GenreDTO> genres, int genre, 
            List<ArtistDTO> artists, int artist)
        {
            Genres = new SelectList(genres, "Id", "Title", genre);
            SelectedGenre = genre;

            Artists = new SelectList(artists, "Id", "Name", artist);
            SelectedArtist = artist;
        }
        public SelectList Genres { get; set; } = new SelectList(new List<GenreDTO>(), "Id", "Title");        
        public int SelectedGenre { get; set; }
        public SelectList Artists { get; set; } = new SelectList(new List<ArtistDTO>(), "Id", "Name");
        public int SelectedArtist { get; set; }
    }
}
