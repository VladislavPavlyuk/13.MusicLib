using Microsoft.EntityFrameworkCore;
using MusicLib.DAL.Entities;
using MusicLib.DAL.Interfaces;
using MusicLib.DAL.EF;

namespace MusicLib.DAL.Repositories
{
    public class GenreRepository : IRepository<Genre>
    {
        private MusicLibContext db;

        public GenreRepository(MusicLibContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await db.Genres.ToListAsync();
        }

        public async Task<Genre> Get(int id)
        {
            Genre? genre = await db.Genres.FindAsync(id);
            return genre!;
        }

        public async Task<Genre> Get(string title)
        {
            var genres = await db.Genres.Where(a => a.Title == title).ToListAsync(); 
            Genre? genre = genres?.FirstOrDefault();
            return genre!;
        }

        public async Task Create(Genre genre)
        {
            await db.Genres.AddAsync(genre);
        }

        public void Update(Genre genre)
        {
            db.Entry(genre).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Genre? genre = await db.Genres.FindAsync(id);

            if (genre != null)          
                db.Genres.Remove(genre);
            
        }

        public async Task<IEnumerable<Genre>> GetSortedAsync(string sortOrder)
        {
            var items = from i in db.Genres
                        select i;

            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(i => i.Title);
                    break;
                default:
                    items = items.OrderBy(i => i.Title);
                    break;
            }

            return await items.ToListAsync();
        }
    }
}
