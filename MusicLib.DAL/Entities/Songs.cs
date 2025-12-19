namespace MusicLib.DAL.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Release { get; set; }
        public string? YoutubeLink { get; set; }

        public Genre? Genre { get; set; }
        public int? GenreId { get; set; }

        public Artist? Artist { get; set; }
        public int? ArtistId { get; set; }

        public Audio? Audio { get; set; }
        public int? AudioId { get; set; }
    }
}
