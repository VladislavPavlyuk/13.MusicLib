using Microsoft.EntityFrameworkCore;
using MusicLib.DAL.Entities;
using MusicLib.DAL.Interfaces;
using MusicLib.DAL.EF;


namespace MusicLib.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private MusicLibContext db;

        public UserRepository(MusicLibContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.Include(o => o.Role).ToListAsync();
        }

        public async Task<User> Get(int id)
        {
            var users = await db.Users.Include(o => o.Role).Where(a => a.Id == id).ToListAsync();

            User? user = users?.FirstOrDefault();

            return user!;
        }

        public async Task<User> Get(string email)
        {         
            var users = await db.Users.Include(o => o.Role).Where(a => a.Email == email).ToListAsync();

            User? user = users?.FirstOrDefault();

            return user!;
        }

        public async Task Create(User user)
        {
            await db.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
                User? user = await db.Users.FindAsync(id);

                if (user != null)
                    db.Users.Remove(user);            
        }
        public async Task<IEnumerable<User>> GetSortedAsync(string sortOrder)
        {
            var items = from i in db.Users
                        select i;

            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(i => i.Email);
                    break;

                default:
                    items = items.OrderBy(i => i.Email);
                    break;
            }

            return await items.ToListAsync();
        }
    }
}
