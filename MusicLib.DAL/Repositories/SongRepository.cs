using Microsoft.EntityFrameworkCore;
using MusicLib.DAL.Entities;
using MusicLib.DAL.Interfaces;
using MusicLib.DAL.EF;


namespace MusicLib.DAL.Repositories
{
    public class SongRepository : IRepository<Song>
    {
        private MusicLibContext db;

        public SongRepository(MusicLibContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Song>> GetAll()
        {
            return await db.Songs.Include(o => o.Genre).
                                    Include(o => o.Artist).
                                    Include(o => o.Audio).ToListAsync();
        }

        public async Task<Song> Get(int id)
        {
            var song = await db.Songs
                .Include(o => o.Genre)
                .Include(o => o.Artist)
                .Include(o => o.Audio)
                .FirstOrDefaultAsync(a => a.Id == id);
            return song!;
        }

        public async Task<Song> Get(string title)
        {         
            var song = await db.Songs
                .Include(o => o.Genre)
                .Include(o => o.Artist)
                .Include(o => o.Audio)
                .FirstOrDefaultAsync(a => a.Title == title);
            return song!;
        }

        public async Task Create(Song song)
        {
            await db.Songs.AddAsync(song);
        }

        public void Update(Song song)
        {
            db.Entry(song).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
                Song? song = await db.Songs.FindAsync(id);
                if (song != null)
                    db.Songs.Remove(song);            
        }

        public async Task<IEnumerable<Song>> GetSortedAsync(string sortOrder)
        {
            var items = from i in await db.Songs.
                        Include(o => o.Genre).
                        Include(o => o.Artist).
                        Include(o => o.Audio).ToListAsync()
            select i;

            switch (sortOrder)
            {
                 case "ArtistNameAsc":
                    items = items.OrderBy(i => i.Artist!.Name);
                    break;
                case "ArtistNameDesc":
                    items = items.OrderByDescending(i => i.Artist!.Name);
                    break;
                case "ArtistBirthDateAsc":
                    items = items.OrderBy(i => i.Artist!.BirthDate);
                    break;
                case "ArtistBirthDateDesc":
                    items = items.OrderByDescending(i => i.Artist!.BirthDate);
                    break;
                case "GenreTitleAsc":
                    items = items.OrderBy(i => i.Genre!.Title);
                    break;
                case "GenreTitleDesc":
                    items = items.OrderByDescending(i => i.Genre!.Title);
                    break;
                /*case "SongTitleAsc":
                    items = items.OrderBy(i => i.Title);
                    break;*/
                case "SongTitleDesc":
                    items = items.OrderByDescending(i => i.Title);
                    break;
                case "SongReleaseDateAsc":
                    items = items.OrderBy(i => i.Release);
                    break;
                case "SongReleaseDateDesc":
                    items = items.OrderByDescending(i => i.Release);
                    break;
                default:
                    items = items.OrderBy(i => i.Title);
                    break;
            }
            return  items;
        }
    }
}
