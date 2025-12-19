using MusicLib.BLL.DTO;

namespace MusicLib.Models
{
    public class IndexViewModel
    {
        public IEnumerable<SongDTO> Songs { get; set; }
        public PageViewModel PageViewModel { get; }
        public FilterViewModel FilterViewModel { get; }
        public SortViewModel SortViewModel { get; }

        public IndexViewModel(
            IEnumerable<SongDTO> songs, 
            PageViewModel pageViewModel,
            FilterViewModel filterViewModel, 
            SortViewModel sortViewModel)
        {
            Songs = songs;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}

