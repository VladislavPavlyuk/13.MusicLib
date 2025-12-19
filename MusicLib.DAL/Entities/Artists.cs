
namespace MusicLib.DAL.Entities
{
    public class Artist
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? BirthDate { get; set; }

        public ICollection<Song>? Songs { get; set; }
    }
}
