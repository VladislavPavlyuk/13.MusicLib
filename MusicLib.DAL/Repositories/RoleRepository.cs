using Microsoft.EntityFrameworkCore;
using MusicLib.DAL.Entities;
using MusicLib.DAL.Interfaces;
using MusicLib.DAL.EF;

namespace MusicLib.DAL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private MusicLibContext db;

        public RoleRepository(MusicLibContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await db.Roles.ToListAsync();
        }

        public async Task<Role> Get(int id)
        {
            Role? role = await db.Roles.FindAsync(id);
            return role!;
        }

        public async Task<Role> Get(string title)
        {
            var roles = await db.Roles.Where(a => a.Title == title).ToListAsync();
            Role? role = roles?.FirstOrDefault();
            return role!;
        }

        public async Task Create(Role role)
        {
            await db.Roles.AddAsync(role);
        }

        public void Update(Role role)
        {
            db.Entry(role).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Role? role = await db.Roles.FindAsync(id);
            if (role != null)
                db.Roles.Remove(role);
        }

        public async Task<IEnumerable<Role>> GetSortedAsync(string sortOrder)
        {
            var items = from i in db.Roles
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
