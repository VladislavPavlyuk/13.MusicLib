namespace MusicLib.Models
{
    public class SortViewModel
    {
        public SortState ArtistNameSort { get; set; } // значение для сортировки по имени артиста
        public SortState ArtistBirthDateSort { get;  set; }    // значение для сортировки по дате рождения по артиста
        public SortState GenreTitleSort { get;  set; }    // значение для сортировки по жанру         
        public SortState SongTitleSort { get;  set; }    // значение для сортировки по названию песни         
        public SortState SongReleaseDateSort { get;  set; }    // значение для сортировки по дате релиза песни 
        public SortState Current { get; set; }     // значение свойства, выбранного для сортировки
        public bool Up { get; set; }  // Сортировка по возрастанию или убыванию
        public SortViewModel(SortState sortOrder)
        {
            // значения по умолчанию
            ArtistNameSort = SortState.ArtistNameAsc;
            ArtistBirthDateSort = SortState.ArtistBirthDateAsc;
            GenreTitleSort = SortState.GenreTitleAsc;
            SongTitleSort = SortState.SongTitleAsc;
            SongReleaseDateSort = SortState.SongReleaseDateAsc;
            Up = true;

            if (sortOrder == SortState.ArtistNameDesc || 
                sortOrder == SortState.ArtistBirthDateDesc || 
                sortOrder == SortState.GenreTitleDesc || 
                sortOrder == SortState.SongTitleDesc ||
                sortOrder == SortState.SongReleaseDateDesc)
            {
                Up = false;
            }

            ArtistNameSort = sortOrder == SortState.ArtistNameAsc ? SortState.ArtistNameDesc : SortState.ArtistNameAsc;
            ArtistBirthDateSort = sortOrder == SortState.ArtistBirthDateAsc ? SortState.ArtistBirthDateDesc : SortState.ArtistBirthDateAsc;
            GenreTitleSort = sortOrder == SortState.GenreTitleAsc ? SortState.GenreTitleDesc : SortState.GenreTitleAsc;
            SongTitleSort = sortOrder == SortState.SongTitleAsc ? SortState.SongTitleDesc : SortState.SongTitleAsc;
            SongReleaseDateSort = sortOrder == SortState.SongReleaseDateAsc ? SortState.SongReleaseDateDesc : SortState.SongReleaseDateAsc;

            switch (sortOrder)
            {
                case SortState.ArtistNameDesc:
                    Current = ArtistNameSort = SortState.ArtistNameDesc;
                    break;
                case SortState.ArtistBirthDateDesc:
                    Current = ArtistBirthDateSort = SortState.ArtistBirthDateAsc;
                    break;
                case SortState.ArtistBirthDateAsc:
                    Current = ArtistBirthDateSort = SortState.ArtistBirthDateDesc;
                    break;
                case SortState.GenreTitleDesc:
                    Current = GenreTitleSort = SortState.GenreTitleAsc;
                    break;
                case SortState.GenreTitleAsc:
                    Current = GenreTitleSort = SortState.GenreTitleDesc;
                    break;
                case SortState.SongTitleDesc:
                    Current = SongTitleSort = SortState.SongTitleAsc;
                    break;
                case SortState.SongTitleAsc:
                    Current = SongTitleSort = SortState.SongTitleDesc;
                    break;
                case SortState.SongReleaseDateDesc:
                    Current = SongReleaseDateSort = SortState.SongReleaseDateAsc;
                    break;
                case SortState.SongReleaseDateAsc:
                    Current = SongReleaseDateSort = SortState.SongReleaseDateDesc;
                break;
                default: 
                    Current = ArtistNameSort = SortState.ArtistNameDesc;
                    break;

            }
        }
    }
}
