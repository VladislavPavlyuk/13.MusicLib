using Microsoft.EntityFrameworkCore;
using MusicLib.DAL.Entities;
using MusicLib.DAL.Interfaces;
using MusicLib.DAL.EF;

namespace MusicLib.DAL.Repositories
{
    public class AudioRepository : IRepository<Audio>
    {
        private MusicLibContext db;
        public AudioRepository(MusicLibContext context)
        {
            this.db = context;
        }
        public async Task<IEnumerable<Audio>> GetAll()
        {
            return await db.Audios.ToListAsync();
        }
        public async Task<Audio> Get(int id)
        {
            Audio? audio = await db.Audios.FindAsync(id);
            return audio!;
        }
        public async Task<Audio> Get(string name)
        {
            var audios = await db.Audios.Where(a => a.FileName == name).ToListAsync();
            Audio? audio = audios?.FirstOrDefault();
            return audio!;
        }
        public async Task Create(Audio audio)
        {
            await db.Audios.AddAsync(audio);
        }
        public void Update(Audio audio)
        {
            db.Entry(audio).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            Audio? audio = await db.Audios.FindAsync(id);
            if (audio != null)
                db.Audios.Remove(audio);
        }
        public async Task<IEnumerable<Audio>> GetSortedAsync(string sortOrder)
        {
            var items = from i in db.Audios
                        select i;

            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(i => i.FileName);
                    break;

                default:
                    items = items.OrderBy(i => i.FileName);
                    break;
            }

            return await items.ToListAsync();
        }
    }
}
